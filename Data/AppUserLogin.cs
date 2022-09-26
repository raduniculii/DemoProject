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
    public class AppUserLogin: AppUserLogin_Base { }
    public class AppUserLogin_Hist: AppUserLogin_Base, IHistFor<AppUserLogin> { }

    public abstract class AppUserLogin_Base: Record
    {
        public string LoginProvider { get; set; } = default!;
        public string ProviderKey { get; set; } = default!;
        public string? ProviderDisplayName { get; set; } = default!;
        public int AppUserId { get; set; } = default!;
        public int AppUserVer { get; set; } = default!;
    }
}