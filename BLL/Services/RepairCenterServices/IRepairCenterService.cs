
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.RepairCenterServices
{
    public interface IRepairCenterService
    {
        Task<IEnumerable<RepairCenter>> GetAllAsync();
        Task<IEnumerable<RepairCenter>> GetCentersByBrandAsync(Guid detectionId, string brand);

        Task<IEnumerable<RepairCenter>> GetCentersByDetectionIdAsync(Guid detectionId);
    }

}
