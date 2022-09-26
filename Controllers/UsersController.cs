using DemoProject.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;
using DemoProject.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Identity;
using DemoProject.Models;
using DemoProject.AppCode;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly AppDbContext db;
        private readonly ILogger<UsersController> logger;
        private readonly ListSettingsForUser listSettings;
        private readonly IEmailSender emailSender;
        private readonly UserManager<AppUser> userManager;
        private readonly UserResolverService userResolver;

        public UsersController(
                AppDbContext db
                , ILogger<UsersController> logger
                , ListSettingsForUser listSettings
                , IEmailSender emailSender
                , UserManager<AppUser> userManager
                , UserResolverService userResolver
            ){
            this.db = db;
            this.logger = logger;
            this.listSettings = listSettings;
            this.emailSender = emailSender;
            this.userManager = userManager;
            this.userResolver = userResolver;
        }

        [HttpGet]
        [HttpGet("{id:int}/{subQuery}")]
        [HttpGet("{subQuery}")]
        public async Task<object> Index(
                [FromQuery(Name = "start")]int? start
                , [FromQuery(Name = "max")]int? max
                , [FromQuery(Name = "o")]string? orderBy
                , [FromQuery(Name = "f")]string? filter
                , int? id
                , string? subQuery
            ){
                subQuery ??= "Main";
                logger.LogDebug($"Getting {subQuery} list");
                bool isMain = (subQuery == "Main");

                logger.LogDebug($"isMain = {isMain}, filter = {filter}, orderBy = {orderBy}, id= {id}, start = {start}, max={max}");

                var items = (
                    from user in (IQueryable<AppUser_Base>)(isMain ? db.AppUsers : db.AppUsers_Hist)
                    select new { user.Id, user.Ver, user.UserName, user.FirstName, user.LastName, FullName = string.Join(" " , new []{ user.FirstName, user.LastName }), user.RoleName, user.Email, user.TwoFactorEnabled, user.TwoFactorRequired, user.HistActionDate, user.HistActionDateString, user.HistActionType,  user.HistActionTypeOrder, user.HistActionPerformedById, user.HistActionPerformedBy }
                );

                if((id ?? 0) != 0) {
                    items = items.Where(i => i.Id == id);
                }
                
                return await Task.FromResult(
                    items.Filter(filter).SortBy(orderBy, nameof(Record.Id))
                        .GetWebApiItemList(start ?? 0, max)
                );
        }

        [HttpGet("{id:int}")]
        public async Task<object> GetItemDetails(int id){
            AppUser_Base theUser = null!;
            theUser = await Task.FromResult((
                from user in db.AppUsers
                where user.Id == id
                select user
            ).First());

            theUser.AppRoleId = (
                from userRole in db.AppUserRoles
                where userRole.AppUserId == theUser.Id
                select userRole.AppRoleId
            ).FirstOrDefault();

            return new AppUserDto{
                Id = theUser.Id
                , Ver = theUser.Ver
                , UserName = theUser.UserName!
                , FirstName = theUser.FirstName!
                , LastName = theUser.LastName!
                , FullName = string.Join(" ", (new[]{ theUser.FirstName, theUser.LastName }).Where(n => n != null))
                , AppRoleId = theUser.AppRoleId.Value
                , Email = theUser.Email!
                , TwoFactorRequired = theUser.TwoFactorRequired
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id){
            var item = await db.AppUsers.FindAsync(id);
            if(id == userResolver.GetUserId()){
                ModelState.AddModelError(nameof(id), "You cannot delete yourself.");
                return ValidationProblem(ModelState);
            }

            if (item == null)
            {
                return NotFound();
            }

            db.AppUserRoles.RemoveRange(db.AppUserRoles.Where(ur => ur.AppUserId == item.Id));
            db.AppUsers.Remove(item);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> SaveItem(Models.AppUserDto item)
        {
            logger.LogDebug("adding item");
            var newItem = new AppUser();

            newItem.Ver = item.Ver;
            newItem.UserName = item.UserName;
            newItem.NormalizedUserName = item.UserName.ToUpper();
            newItem.FirstName = item.FirstName;
            newItem.LastName = item.LastName;
            newItem.Email = item.Email;
            newItem.NormalizedEmail = item.Email.ToUpper();
            newItem.TwoFactorRequired = item.TwoFactorRequired;

            using(var trn = db.Database.BeginTransaction()){
                await userManager.CreateAsync(newItem, "tmp-P4$$");
                logger.LogDebug("added to list, saving");
                try {
                    if(db.AppUsers.Any(u => u.NormalizedUserName == newItem.NormalizedUserName && u.Id != newItem.Id)){
                        ModelState.AddModelError(nameof(newItem.UserName).ToLower(), "This user name is already used.");
                        
                        return ValidationProblem(ModelState);
                    }
                    if(db.AppUsers.Any(u => u.NormalizedEmail == newItem.NormalizedEmail)){
                        ModelState.AddModelError(nameof(newItem.Email).ToLower(), "This email is already used.");
                        
                        return ValidationProblem(ModelState);
                    }

                    await db.SaveChangesAsync();
                    var role = db.AppRoles.Where(r => r.Id == item.AppRoleId).First();
                    newItem.RoleName = role.Name;
                    db.Add(new AppUserRole{ AppUserId = newItem.Id, AppUserVer = newItem.Ver, AppRoleId = item.AppRoleId, AppRoleVer = role.Ver });
                    await db.SaveChangesAsync();
                    logger.LogDebug("saved");
                    
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(newItem);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    string callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = newItem.Id, code = code },
                        protocol: Request.Scheme)!;
                    await emailSender.SendEmailAsync(newItem.Email, "You have a new account in DemoProject.",
                        $"Please confirm your email by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    logger.LogDebug("mail sent");
                }
                catch(DbUpdateConcurrencyException){
                    logger.LogDebug("problem saving");
                    trn.Rollback();
                    return this.ValidationProblem(ModelState);
                }
            }

            return NoContent();
        }

        [HttpPut("{id}"), HttpPost("{id}")]
        public async Task<IActionResult> SaveItem(int id, Models.AppUserDto item)
        {
            if(id != item.Id) return BadRequest();

            var foundItem = await db.AppUsers.FindAsync(id);

            if (foundItem == null)
            {
                return NotFound();
            }

            foundItem.Ver = item.Ver;
            foundItem.UserName = item.UserName;
            foundItem.NormalizedUserName = item.UserName.ToUpper();
            foundItem.FirstName = item.FirstName;
            foundItem.LastName = item.LastName;
            foundItem.TwoFactorRequired = item.TwoFactorRequired;
            if(foundItem.Email != item.Email){
                await userManager.SetEmailAsync(foundItem, item.Email);
                foundItem.NormalizedEmail = item.Email.ToUpper();
            }

            var role = db.AppRoles.Where(r => r.Id == item.AppRoleId).First();
            
            var userRole = db.AppUserRoles.Where(ur => ur.AppUserId == foundItem.Id).First();
            userRole.AppRoleId = role.Id;
            userRole.AppRoleVer = role.Ver;
            
            foundItem.RoleName = role.Name;

            try {
                if(db.AppUsers.Any(u => u.NormalizedUserName == foundItem.NormalizedUserName && u.Id != foundItem.Id)){
                    ModelState.AddModelError(nameof(foundItem.UserName).ToLower(), "This user name is already used.");
                    
                    return ValidationProblem(ModelState);
                }
                if(db.AppUsers.Any(u => u.NormalizedEmail == foundItem.NormalizedEmail && u.Id != foundItem.Id)){
                    ModelState.AddModelError(nameof(foundItem.Email).ToLower(), "This email is already used.");
                    
                    return ValidationProblem(ModelState);
                }

                await db.SaveChangesAsync();
                    
                if(!foundItem.EmailConfirmed){
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(foundItem);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    string callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = foundItem.Id, code = code },
                        protocol: Request.Scheme)!;
                    await emailSender.SendEmailAsync(foundItem.Email, "Email confirmation.",
                        $"Please confirm your email by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    logger.LogDebug("mail sent");
                }
            }
            catch(DbUpdateConcurrencyException){
                ModelState.AddModelError(nameof(item.Ver).ToLower(), "Version mismatch.");
                return this.ValidationProblem(ModelState);
            }

            return NoContent();
        }
    }
}