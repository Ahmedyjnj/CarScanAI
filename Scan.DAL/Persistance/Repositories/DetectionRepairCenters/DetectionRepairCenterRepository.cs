using IKEa.DAL.Persinstance.Data;
using IKEa.DAL.Persinstance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.DetectionRepairCenters
{
    public class DetectionRepairCenterRepository
    : GenericRepository<DetectionRepairCenter>,
      IDetectionRepairCenterRepository
    {
        private readonly ApplicationDbContext _context;

        public DetectionRepairCenterRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetectionRepairCenter>>
            GetByDetectionIdAsync(Guid detectionId)
        {
            return await _context.DetectionRepairCenters
                .Include(d => d.RepairCenter)
                .Where(d => d.DetectionId == detectionId)
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<DetectionRepairCenter> entities)
        {
            await _context.DetectionRepairCenters.AddRangeAsync(entities);
        }
    }

}
