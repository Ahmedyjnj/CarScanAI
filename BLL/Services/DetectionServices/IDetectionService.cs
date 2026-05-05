
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.DetectionServices
{
    public interface IDetectionService
    {
        Task<Detection> CreateDetectionAsync(Guid carId , string imagepath);
        Task<Detection> GetDetectionAsync(Guid detectionId);
    }
}
