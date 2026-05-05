
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.DamageServices
{
    public interface IDamageService
    {
        Task<IEnumerable<DamageDetail>> GetByDetectionIdAsync(Guid detectionId);

    }
}
