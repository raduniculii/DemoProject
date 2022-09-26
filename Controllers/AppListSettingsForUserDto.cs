namespace DemoProject.Models
{
    public class AppListSettingsForUserDto
    {
        public string ListProgName { get; set; } = default!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; } = default!;
        public string QuickSearch { get; set; } = default!;
        public string Search { get; set; } = default!;
    }
}