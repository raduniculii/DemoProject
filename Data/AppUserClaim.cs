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
    public class AppUserClaim: AppUserClaim_Base { }
    public class AppUserClaim_Hist: AppUserClaim_Base, IHistFor<AppUserClaim> { }

    public abstract class AppUserClaim_Base: Record
    {
        public int AppUserId { get; set; } = default!;
        public int AppUserVer { get; set; } = default!;
        public string? ClaimType { get; set; } = default!;
        public string? ClaimValue { get; set; } = default!;
        public string Issuer { get; set; } = default!;
    }
}