using Microsoft.AspNetCore.Mvc;
using Scan.BLL.Services.RepairCenterServices;
using System.Threading.Tasks;

namespace Scan.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairCentersController(IRepairCenterService repairCenterService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var centers= await repairCenterService.GetAllAsync();

            return Ok(centers);
        }
    }
}
