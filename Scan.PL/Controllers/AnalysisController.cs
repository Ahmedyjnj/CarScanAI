using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scan.BLL.Dto_s;
using Scan.BLL.Dto_s.AnalysisDto_s;
using Scan.BLL.Services.AnalysisServices;
using Scan.BLL.Services.CarServices;
using System.Security.Claims;


namespace Scan.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController(IAnalysisService _analysisService, ICarService _carService) : ControllerBase
    {
        [HttpPost("analyzenewcar")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AnalyzeNew(
     [FromForm] AnalyzeRequestNewDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(
                    string.Join(" | ",
                    ModelState.Values
                    .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage)));


            }

            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException(
                    "Invalid authentication token.");

            var createdCar =
                await _carService.AddCarAsync(
                    dto.CarDto,
                    userId);

            var result =
                await _analysisService.AnalyzeAsync(
                   
                    createdCar.CarId,
                    dto.Image,
                    userId
                     );

            return Ok(new
            {
                Status = 200,
                Message = "Analysis completed successfully.",
                Data = result
            });
        }

        [HttpPost("analyze")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Analyze(
    [FromForm] AnalyzeRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(
                    string.Join(" | ",
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
            }

            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException(
                    "Invalid authentication token.");
            }

            var result =
                await _analysisService.AnalyzeAsync(
                    dto.CarId,
                    dto.Image,
                    userId
                    );

            return Ok(new
            {
                Status = 200,
                Message =
                    "Analysis completed successfully.",
                Data = result
            });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAnalysis() //get All 
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException(
                    "Invalid authentication token.");


            var result = await _analysisService
                .GetAll(userId);

            return Ok(result);
        }
    }
}
