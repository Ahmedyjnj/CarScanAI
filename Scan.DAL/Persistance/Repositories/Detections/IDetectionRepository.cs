using IKEa.DAL.Persinstance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.Detections
{
    public interface IDetectionRepository
    : IGenericRepository<Detection>
    {
        Task<Detection?> GetFullDetectionAsync(Guid detectionId);
    }

}
