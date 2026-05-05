
using Microsoft.EntityFrameworkCore;
using Scan.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.DamageServices
{
    public class DamageService(IUnitOfWork unitOfWork) : IDamageService
    {
        public async Task<IEnumerable<DamageDetail>> GetByDetectionIdAsync(Guid detectionId)
        {
            return await unitOfWork.GetRepository<DamageDetail>().GetAll()
                .Where(d => d.DetectionId == detectionId)
                .ToListAsync();
        }
    }
}
