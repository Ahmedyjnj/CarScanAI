using IKEa.DAL.Persinstance.Data;
using IKEa.DAL.Persinstance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.RepairCenters
{
    public class RepairCenterRepository
    : GenericRepository<RepairCenter>, IRepairCenterRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairCenterRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RepairCenter>> GetActiveCentersAsync()
        {
            return await _context.RepairCenters
                .Where(r => r.IsActive)
                .ToListAsync();
        }
    }

}
