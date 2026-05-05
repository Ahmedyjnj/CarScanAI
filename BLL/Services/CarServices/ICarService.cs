using Scan.BLL.Dto_s;
using Scan.BLL.Dto_s.CarDto_s;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scan.BLL.Services.CarServices
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetUserCarsAsync(string userId);
        Task<CarDto?> GetCarByIdAsync(Guid carId);
        Task<CarDto> AddCarAsync(CreateCarDto dto, string userId);
        Task UpdateCarAsync(Guid carId, CarDto dto, string userId);
        Task DeleteCarAsync(Guid carId, string userId);
    }
}
