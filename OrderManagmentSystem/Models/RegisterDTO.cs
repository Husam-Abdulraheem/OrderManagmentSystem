﻿using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class RegisterDTO
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? BusinessName { get; set; }

        public IFormFile? BusinessDocument { get; set; }

    }
}
