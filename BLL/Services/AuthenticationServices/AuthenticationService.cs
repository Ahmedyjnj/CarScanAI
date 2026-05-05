using AutoMapper;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scan.BLL.Dto_s;
using Scan.BLL.Services.Attachments;
using Scan.BLL.Services.EmailServices;
using Shared.Dto_s.IdentityDto_s;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Scan.BLL.Services.AuthenticationServices
{
    public class AuthenticationService(UserManager<User> userManager, IConfiguration configuration,
        IMapper mapper, IEmailService emailService, IFileService fileService) : IAuthenticationServices
    {
        public async Task<UserSimpleDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UserNotFoundException(loginDto.Email);

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                throw new UnauthorizedException("invalid password");

            var dto = mapper.Map<UserSimpleDto>(user);
            dto.Token = await CreateTokenAsync(user);
            return dto;
        }

        public async Task<UserProfileDto> RegisterAsync([FromForm] RegisterDto registerDto)
        {
            var user = mapper.Map<User>(registerDto);

            if (registerDto.Image != null)
            {
                user.ProfileImage = await fileService.UploadAsync(registerDto.Image); ;
            }
            else
            {

                user.ProfileImage = null;

            }
            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                return mapper.Map<UserProfileDto>(user);

            var errors = result.Errors
            .Select(e => e.Description)
            .ToList();

            var oneLine = errors.Count > 0
                ? "Registration failed: " + string.Join(". ", errors)
                : "Registration failed.";

            throw new BadRequestException(oneLine);
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<UserProfileDto> GetCurrentUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException(userId);
            return mapper.Map<UserProfileDto>(user);
        }

        public async Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ForgotPasswordResult
                {
                    Success = true,
                    Message = "If an account exists for this email, you will receive a password reset link."
                };
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var baseUrl = configuration["Frontend:BaseUrl"];
            var resetPath = configuration["Frontend:ResetPasswordPath"];

            var resetLink = $"{baseUrl}/{resetPath}?email={user.Email}&token={Uri.EscapeDataString(token)}";


            await emailService.SendAsync(
             user.Email,
             "Reset Your Password",
            $"Click <a href='{resetLink}'>here</a> to reset your password."
                );

            string setting = configuration.GetSection("ForgotPassword:ReturnTokenInResponse").Value.ToString();
            bool returnTokenInResponse; //this is only for development/testing. In production, the token should be sent via email and not returned in the response.

            if (setting.ToLower() == "true")
            {
                returnTokenInResponse = true;
            }
            else
            {
                returnTokenInResponse = false;
            }

            return new ForgotPasswordResult
            {
                Success = true,
                Message = "If an account exists for this email, you will receive a password reset link.",
                Token = returnTokenInResponse ? token : null
            };
        }

        public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ResetPasswordResult
                {
                    Success = false,
                    Message = "Invalid request.",
                    Errors = new[] { "Invalid email or token." }
                };
            }

            var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (result.Succeeded)
            {
                return new ResetPasswordResult
                {
                    Success = true,
                    Message = "Your password has been reset successfully."
                };
            }

            return new ResetPasswordResult
            {
                Success = false,
                Message = "Failed to reset password.",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),

            };

            // var Roles = await userManager.GetRolesAsync(user);

            //foreach (var role in Roles)
            //{
            //    Claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var SecretKey = configuration.GetSection("JwtOptions")["SecretKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));



            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken
             (
               issuer: configuration["JwtOptions:Issuer"],
               audience: configuration["JwtOptions:Audience"],
               claims: Claims,
               expires: DateTime.UtcNow.AddDays(3),
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);



        }

    }
}
