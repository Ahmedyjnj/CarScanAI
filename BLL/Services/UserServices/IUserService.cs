using Scan.BLL.Dto_s;
using System.Threading.Tasks;

namespace Scan.BLL.Services.User_Services
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetProfileAsync(string userId);
        Task UpdateProfileAsync(string userId, UpdateProfileDto dto);
    }
}
