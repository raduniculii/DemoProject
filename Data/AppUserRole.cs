using DemoProject.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DemoProject.Data
{
    public class AppUserRole: AppUserRole_Base { }
    public class AppUserRole_Hist: AppUserRole_Base, IHistFor<AppUserRole> { }

    public abstract class AppUserRole_Base: Record
    {
        public int AppUserId { get; set; } = default!;
        public int AppUserVer { get; set; } = default!;
        public int AppRoleId { get; set; } = default!;
        public int AppRoleVer { get; set; } = default!;
    }
}