using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s.AnalysisDto_s
{
    public class RepairCenterRecommendationDto
    {
        public Guid CenterId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }


    }
}
