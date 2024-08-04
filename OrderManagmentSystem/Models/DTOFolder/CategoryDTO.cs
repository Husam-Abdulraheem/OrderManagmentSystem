using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models.DTOFolder
{
    public class CategoryDTO
    {
        [MinLength(2), StringLength(50)]
        public string Name { get; set; }

        public IFormFile? Image { get; set; }
    }
}
