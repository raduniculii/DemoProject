using DemoProject.Data.Common;

namespace DemoProject.Data
{
    public class AppClaimType: AppClaimType_Base { }
    public class AppClaimType_Hist: AppClaimType_Base, IHistFor<AppClaimType> { }

    public abstract class AppClaimType_Base: Record
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}