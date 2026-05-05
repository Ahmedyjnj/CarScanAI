using Scan.BLL.Dto_s.AiDto;
using Scan.BLL.Dto_s.AiResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.Ai
{
    public interface ICarDamageAiService
    {
        Task<AiResponseDto> GenerateAiDamageImageAsync(AiRequestDto aiRequestDto);
    }
}
