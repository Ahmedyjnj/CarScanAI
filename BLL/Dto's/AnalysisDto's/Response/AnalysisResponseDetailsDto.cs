using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s.AnalysisDto_s
{
    public class AnalysisResponseDetailsDto
    {
        public Guid AnalysisId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public decimal TotalEstimatedCost { get; set; } //need to calculate it by ai and severity and name of object 

        public AnalysisFindingDto Finding { get; set; } 
        public List<RepairCenterRecommendationDto> Recommendations { get; set; } = new();

    }
   
}
