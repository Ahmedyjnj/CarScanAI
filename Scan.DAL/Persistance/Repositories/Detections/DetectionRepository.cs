using IKEa.DAL.Persinstance.Data;
using IKEa.DAL.Persinstance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.Detections
{
    public class DetectionRepository
      : GenericRepository<Detection>, IDetectionRepository
    {
        private readonly ApplicationDbContext _context;

        public DetectionRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Detection?> GetFullDetectionAsync(Guid detectionId)
        {
            return await _context.Detections
                .Include(d => d.DamageDetails)
                .Include(d => d.DetectionRepairCenters)
                    .ThenInclude(dr => dr.RepairCenter)
                .FirstOrDefaultAsync(d => d.DetectionId == detectionId);
        }
    }

}
