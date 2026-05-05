using Scan.BLL.Dto_s;
using Shared.Dto_s.IdentityDto_s;

namespace Scan.BLL.Services.AuthenticationServices
{
    public interface IAuthenticationServices
    {
        Task<UserSimpleDto> LoginAsync(LoginDto loginDto);
        Task<UserProfileDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailAsync(string email);
        Task<UserProfileDto> GetCurrentUserAsync(string userId);

        /// <summary>Generates a password reset token for the given email. In production, send it by email.</summary>
        Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordDto dto);

        /// <summary>Resets the user password using the token from ForgotPassword.</summary>
        Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordDto dto);
    }

   
}
