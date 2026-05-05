using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scan.BLL.Dto_s;
using Scan.BLL.Services.AuthenticationServices;
using Scan.BLL.Services.User_Services;
using Shared.Dto_s.IdentityDto_s;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IAuthenticationServices authenticationServices, IUserService userService) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserSimpleDto>> Login(LoginDto loginDto)
        {
            var result = await authenticationServices.LoginAsync(loginDto);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserProfileDto>> Register([FromForm]RegisterDto registerDto)
        {
            var user = await authenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserProfileDto>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var profile = await userService.GetProfileAsync(userId);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var isExist = await authenticationServices.CheckEmailAsync(email);
            return Ok(isExist);
        }



        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<ForgotPasswordResult>> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = await authenticationServices.ForgotPasswordAsync(dto);
            return Ok(result);
        }

        /// <summary>Reset password using the token received from ForgotPassword.</summary>
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ResetPasswordResult>> ResetPassword(ResetPasswordDto dto)
        {
            var result = await authenticationServices.ResetPasswordAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


    }
}
