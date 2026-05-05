using IKEa.DAL.Persinstance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.DamageDetailRepositories
{
    public interface IDamageDetailRepository
    : IGenericRepository<DamageDetail>
    {
        Task<IEnumerable<DamageDetail>> GetByDetectionIdAsync(Guid detectionId);
    }

}
