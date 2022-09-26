using DemoProject.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListSettingsForUser: ControllerBase
    {
        private readonly AppDbContext db;
        private readonly ILogger<ListSettingsForUser> logger;
        private readonly UserResolverService userResolver;

        public ListSettingsForUser(AppDbContext db, ILogger<ListSettingsForUser> logger, UserResolverService userResolver){
            this.db = db;
            this.logger = logger;
            this.userResolver = userResolver;
        }

        [HttpGet]
        public async Task<object> Index(
                string listProgName
            ){
                logger.LogDebug("getting list info");
                var userId = userResolver.GetUserId() ?? 0;
                if(userId == 0) return Task.FromResult<object?>(null);

                var listProgNames = new[]{ listProgName, $"{listProgName}_Hist", $"{listProgName}_Deleted" };
                
                var settings = await (
                    from uls in db.ListSettingsForUsers
                    where uls.AppUserId == userId && listProgNames.Contains(uls.ListProgName)
                    select new { uls.ListProgName, uls.PageNumber, uls.PageSize, uls.SortBy, uls.QuickSearch, uls.Search }
                ).ToListAsync();

                foreach (var lpn in listProgNames)
                {
                    if(settings.FirstOrDefault(s => s.ListProgName == lpn) == null){
                        settings.Add(new { ListProgName = lpn, PageNumber = 1, PageSize = 10, SortBy = "", QuickSearch = "", Search = "" });
                    }
                }

                return settings;
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveItem(Models.AppListSettingsForUserDto listData)
        {
            var userId = userResolver.GetUserId() ?? 0;
            if(userId == 0) return this.Unauthorized();

            logger.LogInformation($@"
ListProgName = {listData.ListProgName}
PageNumber = {listData.PageNumber}
PageSize = {listData.PageSize}
SortBy = {listData.SortBy}
QuickSearch = {listData.QuickSearch}
Search = {listData.Search}
            ");

            var item = db.ListSettingsForUsers.FirstOrDefault(uls => uls.AppUserId == userId && uls.ListProgName == listData.ListProgName);
            if(item == null){
                var userVer = db.AppUsers.First(u => u.Id == userId).Ver;
                db.Add(new Data.ListSettingsForUser{
                    Ver = 1
                    , AppUserId = userId
                    , AppUserVer = userVer
                    , ListProgName = listData.ListProgName
                    , PageNumber = listData.PageNumber
                    , PageSize = listData.PageSize
                    , SortBy = listData.SortBy
                    , QuickSearch = listData.QuickSearch
                    , Search = listData.Search
                });
            }
            else {
                item.PageNumber = listData.PageNumber;
                item.PageSize = listData.PageSize;
                item.SortBy = listData.SortBy;
                item.QuickSearch = listData.QuickSearch;
                item.Search = listData.Search;
            }

            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}