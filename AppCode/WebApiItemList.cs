namespace DemoProject.AppCode
{
    public class WebApiItemList<T>
    {
        public int TotalCount { get; set; }
        public int FirstItemIndex { get; set; }
        public IEnumerable<T> Items { get; set; } = default!;
    }
}