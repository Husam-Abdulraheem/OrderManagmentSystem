using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagmentSystem.Models
{
    [Table("UserTable")]
    public class User
    {
        [Key]
        public long Id { get; set; } = DateTime.UtcNow.Ticks;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(120)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(120)]
        public string businessName { get; set; }

        public Address? Addresses { get; set; }

        [Required]
        public string UserType { get; set; }

        [Required]
        public string businessDocument { get; set; }

        public string? Logo { get; set; }

        public bool IsAdmin { get; set; } = false;

        public bool IsVerified { get; set; } = false;
    }
}
