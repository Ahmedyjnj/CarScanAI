using System.ComponentModel.DataAnnotations;

namespace Shared.Dto_s.IdentityDto_s
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;
    }
    public class ForgotPasswordResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        /// <summary>Only set in Development for testing. In production send via email.</summary>
        public string? Token { get; set; }
    }

  
}
