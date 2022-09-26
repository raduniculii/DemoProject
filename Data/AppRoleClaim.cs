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
    public class AppRoleClaim: AppRoleClaim_Base { }
    public class AppRoleClaim_Hist: AppRoleClaim_Base, IHistFor<AppRoleClaim> { }

    public abstract class AppRoleClaim_Base: Record
    {
        public int AppRoleId { get; set; } = default!;
        public int AppRoleVer { get; set; } = default!;
        public string? ClaimType { get; set; } = default!;
        public string? ClaimValue { get; set; } = default!;
    }
}