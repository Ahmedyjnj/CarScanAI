using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.IdentityDto_s
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
       
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(64, ErrorMessage = "User name must not exceed 64 characters.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(100, ErrorMessage = "Display name must not exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone number must not exceed 20 characters.")]
        public string PhoneNumber { get; set; } = null!;

        public IFormFile? Image { get; set; } 
    }
}
