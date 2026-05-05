
using Domain.Exceptions;
using Scan.DAL.Models.Car;
using Scan.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.DetectionServices
{
    public class DetectionService(IUnitOfWork unitOfWork) : IDetectionService
    {

        public async Task<Detection> CreateDetectionAsync(Guid carId,string imagepath)
        {
            var car = await unitOfWork.GetRepository<Car>().GetById(carId);
            if (car == null)
                throw new NotFoundException("Car not found.");

            var detection = new Detection
            {
                DetectionId = Guid.NewGuid(),
                CarId = carId,
                UserId = car.UserId,
                ImagePath = imagepath,
                DetectionDate = DateTime.UtcNow,
                OverallSeverity = "Unknown",
                TotalCost = 0,
                AiModel = "DefaultModel",



            };

            await unitOfWork.GetRepository<Detection>().AddAsync(detection);
            await unitOfWork.SaveAsync();

             return detection;
        }

      

       public async Task<Detection> GetDetectionAsync(Guid detectionId)
        {
            return await unitOfWork.GetRepository<Detection>().GetById(detectionId);
        }



    }
}
