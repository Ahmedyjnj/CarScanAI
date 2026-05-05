using IKEa.DAL.Persinstance.Data;
using IKEa.DAL.Persinstance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.DamageDetailRepositories
{
    public class DamageDetailRepository
     : GenericRepository<DamageDetail>, IDamageDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public DamageDetailRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DamageDetail>> GetByDetectionIdAsync(Guid detectionId)
        {
            return await _context.DamageDetails
                .Where(dd => dd.DetectionId == detectionId)
                .ToListAsync();
        }
    }

}
