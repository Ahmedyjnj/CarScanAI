using AutoMapper;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Scan.BLL.Dto_s;
using Scan.BLL.Dto_s.CarDto_s;
using Scan.DAL.Models.Car;
using Scan.DAL.Persistance.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scan.BLL.Services.CarServices
{
    public class CarService(IUnitOfWork unitOfWork, IMapper mapper) : ICarService
    {
        public async Task<CarDto> AddCarAsync(CreateCarDto dto, string userId)
        {
           // dto.CarId = Guid.NewGuid();
            var car = mapper.Map<Car>(dto);
            car.UserId = userId;

            try
            {
                await unitOfWork.GetRepository<Car>().AddAsync(car);
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Plate number already exists.");
            }

            return mapper.Map<CarDto>(car);
        }

        public async Task DeleteCarAsync(Guid carId, string userId)
        {
            var car = await unitOfWork.GetRepository<Car>().GetById(carId);
            if (car == null || car.UserId != userId)
                throw new NotFoundException("Car not found.");

            unitOfWork.GetRepository<Car>().Delete(car);
            await unitOfWork.SaveAsync();
        }

        public async Task<CarDto?> GetCarByIdAsync(Guid carId)
        {
            var car = await unitOfWork.GetRepository<Car>().GetById(carId);

            if (car == null) throw new NotFoundException("Car not found.");

            CarDto carDto = mapper.Map<CarDto>(car);

            return carDto ;
        }

        public async Task<IEnumerable<CarDto>> GetUserCarsAsync(string userId)
        {
            var cars = await unitOfWork.GetRepository<Car>()
                .GetAll()
                .Where(c => c.UserId == userId)
                .ToListAsync();

           

            return mapper.Map<IEnumerable<CarDto>>(cars);
        }

        public async Task UpdateCarAsync(Guid carId, CarDto dto, string userId)
        {
            var previous = await unitOfWork.GetRepository<Car>().GetById(carId);

            if (previous == null || previous.UserId != userId)
                throw new NotFoundException("Car not found.");

            mapper.Map(dto, previous);

            unitOfWork.GetRepository<Car>().update(previous);
            await unitOfWork.SaveAsync();
        }
    }
}
