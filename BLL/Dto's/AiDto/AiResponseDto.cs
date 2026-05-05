using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s.AiResponseDto
{
    public class AiResponseDto
    {
       // public string LocationLabel { get; set; } = "front";
        public string prediction { get; set; }
       // public decimal EstimatedCost { get; set; } = 2000;
        public decimal Confidence { get; set; }



       
    }
}
