using DemoProject.Data.Common;

namespace DemoProject.Data
{
    public class ListSettingsForUser: ListSettingsForUser_Base { }
    public class ListSettingsForUser_Hist: ListSettingsForUser_Base, IHistFor<ListSettingsForUser> { }

    public abstract class ListSettingsForUser_Base: Record
    {
        public int AppUserId { get; set; }
        public int AppUserVer { get; set; }
        public string ListProgName { get; set; } = default!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; } = String.Empty;
        public string QuickSearch { get; set; } = String.Empty;
        public string Search { get; set; } = String.Empty;
    }
}