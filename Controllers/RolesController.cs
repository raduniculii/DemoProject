using DemoProject.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using DemoProject.Data;
using DemoProject.AppCode;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController: ControllerBase
    {
        private readonly AppDbContext db;
        private readonly ILogger<RolesController> logger;
        private readonly ListSettingsForUser listSettings;
        private readonly LinkGenerator lnkGen;

        public RolesController(
                AppDbContext db
                , ILogger<RolesController> logger
                , ListSettingsForUser listSettings
                , LinkGenerator lnkGen
            )
        {
            this.db = db;
            this.logger = logger;
            this.listSettings = listSettings;
            this.lnkGen = lnkGen;
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

                var items = (
                    from role in (IQueryable<AppRole_Base>)(isMain ? db.AppRoles : db.AppRoles_Hist)
                    select new {
                        role.Id
                        , role.Ver
                        , role.Name
                        , role.Description
                        , role.Permissions
                        , role.HistActionDate
                        , role.HistActionDateString
                        , role.HistActionType
                        , role.HistActionTypeOrder
                        , role.HistActionPerformedById
                        , role.HistActionPerformedBy
                    }
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
        public async Task<object> GetItemDetails(int id, string? subFolder = null){
            var theRole = await Task.FromResult((
                from role in db.AppRoles
                where role.Id == id
                select role
            ).FirstOrDefault());

            if(theRole == null) return NotFound();

            theRole.Claims = (
                from roleClaim in db.AppRoleClaims
                where roleClaim.AppRoleId == theRole.Id
                select roleClaim.ClaimType
            ).AsEnumerable();

            return theRole;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id){
            var item = await db.AppRoles.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            if(db.AppUserRoles.Any(ur => ur.AppRoleId == id)){
                ModelState.AddModelError(nameof(id).ToLower(), "This role is being used.");
                return ValidationProblem(ModelState);
            }

            db.AppRoleClaims.RemoveRange(db.AppRoleClaims.Where(rc => rc.AppRoleId == id));
            db.AppRoles.Remove(item);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> SaveItem(Models.AppRoleDto item)
        {
            logger.LogDebug("adding item");
            var newItem = new AppRole();

            newItem.Ver = item.Ver;
            newItem.Name = item.Name;
            newItem.NormalizedName = item.Name?.ToLower();
            newItem.Description = item.Description;
            newItem.Permissions = string.Join(", ", item.Claims);

            db.Add(newItem);
            logger.LogDebug("added to list, saving");

            using(var trn = db.Database.BeginTransaction()){
                try {
                    if(db.AppRoles.Any(r => r.Name == newItem.Name && r.Id != newItem.Id)){
                        ModelState.AddModelError(nameof(item.Name).ToLower(), "This role name is already used.");
                        return ValidationProblem(ModelState);
                    }
                    await db.SaveChangesAsync();
                    foreach (var c in item.Claims)
                    {
                        db.Add(new AppRoleClaim(){ AppRoleId = newItem.Id, AppRoleVer = newItem.Ver, ClaimType = c, ClaimValue = "" });
                    }
                    await db.SaveChangesAsync();
                    logger.LogDebug("saved");
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
        public async Task<IActionResult> SaveItem(int id, Models.AppRoleDto item)
        {
            if(id != item.Id) return BadRequest();

            var foundItem = await db.AppRoles.FindAsync(id);

            if (foundItem == null)
            {
                return NotFound();
            }

            foundItem.Ver = item.Ver;
            if(foundItem.Name != item.Name){
                foundItem.Name = item.Name;
                
                await (
                    from ur in db.AppUserRoles
                    where ur.AppRoleId == id
                    join u in db.AppUsers on ur.AppUserId equals u.Id
                    select u
                ).ForEachAsync(u => u.RoleName = item.Name);
            }
            foundItem.NormalizedName = item.Name?.ToLower();
            foundItem.Description = item.Description;
            foundItem.Permissions = string.Join(", ", item.Claims);

            var existingClaims = (
                from roleClaim in db.AppRoleClaims
                where roleClaim.AppRoleId == foundItem.Id
                select roleClaim
            ).AsEnumerable();

            foreach (var rc in existingClaims)
            {
                if(!item.Claims.Contains(rc.ClaimType ?? "")){
                    db.Remove(rc);
                }
            }

            foreach (var c in item.Claims)
            {
                if(existingClaims.FirstOrDefault(ec => ec.ClaimType == c) == null){
                    db.Add(new AppRoleClaim(){ AppRoleId = foundItem.Id, AppRoleVer = foundItem.Ver, ClaimType = c, ClaimValue = "" });
                }
            }

            try {
                if(db.AppRoles.Any(r => r.Name == foundItem.Name && r.Id != foundItem.Id)){
                    ModelState.AddModelError(nameof(foundItem.Name).ToLower(), "This role name is already used.");
                    
                    return ValidationProblem(ModelState);
                }
                await db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                ModelState.AddModelError(nameof(item.Ver).ToLower(), "Version mismatch.");
                return this.ValidationProblem(ModelState);
            }

            return NoContent();
        }
    }
}