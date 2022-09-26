using System.ComponentModel.DataAnnotations;
using DemoProject.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Models
{
    public class AppRoleDto {
        public int Id { get; set; }
        public int Ver { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Display(Name = "Permissions")]
        public List<string> Claims { get; set; } = new List<string>();
    }
}