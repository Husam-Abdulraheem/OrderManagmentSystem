using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Data.Models
{
    [Table("UserTable")]
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [StringLength(120)]
        public string? BusinessName { get; set; }

        public virtual Address? Addresses { get; set; }

        [Required]
        public string? UserType { get; set; }

        public string? BusinessDocument { get; set; }

        public string? LogoUrl { get; set; }

        public bool IsAdmin { get; set; } = false;

        public bool IsVerified { get; set; } = false;
    }
}
