using AutoMapper;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Scan.BLL.Dto_s;
using Scan.DAL.Persistance.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Scan.BLL.Services.User_Services
{
    public class UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper) : IUserService
    {
        public async Task<UserProfileDto?> GetProfileAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            return mapper.Map<UserProfileDto>(user);
        }

        public async Task UpdateProfileAsync(string userId, UpdateProfileDto dto)
        {
            var user = await userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException("User not found.");

            mapper.Map(dto, user);
            await userManager.UpdateAsync(user);
        }
    }
}
