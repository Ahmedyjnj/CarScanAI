using System.ComponentModel.DataAnnotations;

namespace Shared.Dto_s.IdentityDto_s
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Reset token is required.")]
        public string Token { get; set; } = null!;

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;
    }
    public class ResetPasswordResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public IReadOnlyList<string>? Errors { get; set; }
    }
}
