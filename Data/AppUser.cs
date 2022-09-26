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
    public class AppUser: AppUser_Base { }
    public class AppUser_Hist: AppUser_Base, IHistFor<AppUser> { }

    public abstract class AppUser_Base: Record
    {
        public string? UserName { get; set; } = default!;
        public string? NormalizedUserName { get; set; } = default!;
        public string? PasswordHash { get; set; } = default!;
        public string? SecurityStamp { get; set; } = default!;
        public bool TwoFactorEnabled { get; set; } = default!;
        public bool LockoutEnabled { get; set; } = default!;
        public DateTime? LockoutEnd { get; set; } = default!;
        public int AccessFailedCount { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? NormalizedEmail { get; set; } = default!;
        public bool EmailConfirmed { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public bool PhoneNumberConfirmed { get; set; } = default!;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RoleName { get; set; }
        public bool TwoFactorRequired { get; set; }

        [NotMapped]
        public int? AppRoleId { get; set; }
    }
}