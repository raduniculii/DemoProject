using System.ComponentModel.DataAnnotations;
using DemoProject.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Models
{
    public class AppUserDto {
        public int Id { get; set; }
        public int Ver { get; set; }
        
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = default!;

        [MaxLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = default!;

        [Display(Name = "Full name")]
        public string? FullName { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int AppRoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = default!;

        [Display(Name = "Two factor required")]
        public bool TwoFactorRequired { get; set; } = default!;
    }
}