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
    public class AppRole: AppRole_Base { }
    public class AppRole_Hist: AppRole_Base, IHistFor<AppRole> { }

    public abstract class AppRole_Base: Record
    {
        public string? Name { get; set; } = default!;
        public string? NormalizedName { get; set; } = default!;
        public string? Description { get; set; } = default!;

        public string? Permissions { get; set; } = default!;
        [NotMapped]
        public IEnumerable<string>? Claims { get; set; } = default!;
    }
}