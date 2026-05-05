using IKEa.DAL.Persinstance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.DetectionRepairCenters
{
    public interface IDetectionRepairCenterRepository
    : IGenericRepository<DetectionRepairCenter>
    {
        Task<IEnumerable<DetectionRepairCenter>> GetByDetectionIdAsync(Guid detectionId);

        Task  AddRangeAsync(IEnumerable<DetectionRepairCenter> entities);
    }

}
