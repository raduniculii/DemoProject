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
    public class AppUserToken: AppUserToken_Base { }
    public class AppUserToken_Hist: AppUserToken_Base, IHistFor<AppUserToken> { }

    public abstract class AppUserToken_Base: Record
    {
        public int AppUserId { get; set; } = default!;
        public int AppUserVer { get; set; } = default!;
        public string LoginProvider { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Value { get; set; } = default!;
    }
}