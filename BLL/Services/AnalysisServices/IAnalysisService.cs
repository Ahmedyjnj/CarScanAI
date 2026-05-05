using Microsoft.AspNetCore.Http;
using Scan.BLL.Dto_s.AnalysisDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.AnalysisServices
{
    public interface IAnalysisService
    {
        Task<AnalysisResponseDetailsDto> AnalyzeAsync(Guid carId, IFormFile image, string userId);


        public  Task<IEnumerable<AnalysisResponseDetailsDto>> GetAll(string userId);


      //  Task<AnalysisResponseDto> GetByIdAsync(Guid analysisId, string userId);

    }
}
