using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagmentSystem.Models
{

    [Table("AddressTable")]
    public class Address
    {
        [Key]
        public long Id { get; set; } = DateTime.UtcNow.Ticks;

        [Required]
        [StringLength(100)]
        public string city { get; set; }

        [Required]
        [StringLength(100)]
        public string district { get; set; }

        [Required]
        [StringLength(100)]
        public string region { get; set; }

        [Required]
        [StringLength(100)]
        public string street { get; set; }

        [Required]
        [StringLength(250)]
        public string details { get; set; }

    }

}