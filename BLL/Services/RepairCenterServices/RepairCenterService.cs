
using Microsoft.EntityFrameworkCore;
using Scan.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.RepairCenterServices
{
    public class RepairCenterService(IUnitOfWork unitOfWork) : IRepairCenterService
    {
        public async Task<IEnumerable<RepairCenter>> GetCentersByBrandAsync(Guid detectionId, string brand)
        {
            var centers = await unitOfWork.GetRepository<RepairCenter>()
                .GetAll()
                .Where(c => c.IsActive &&
                            c.SupportedBrand.Trim().ToLower().Contains(brand.Trim().ToLower()))
                .ToListAsync();

            var connections = centers.Select(center => new DetectionRepairCenter
            {
                DetectionId = detectionId,
                CenterId = center.CenterId,
                IsContacted = false
            }).ToList();

            await unitOfWork.GetRepository<DetectionRepairCenter>()
                .AddRangeAsync(connections);

            await unitOfWork.SaveAsync();

            return centers;
        }



        public async Task<IEnumerable<RepairCenter>> GetAllAsync()
        {
            return unitOfWork.GetRepository<RepairCenter>().GetAll().ToList();
        }

        public async Task<IEnumerable<RepairCenter>> GetCentersByDetectionIdAsync(Guid detectionId)
        {
            return await unitOfWork
                .GetRepository<DetectionRepairCenter>()
                .GetAll()
                .Include(x => x.RepairCenter)
                .Where(x => x.DetectionId == detectionId)
                .Select(x => x.RepairCenter)
                .ToListAsync();
        }
    }
}
