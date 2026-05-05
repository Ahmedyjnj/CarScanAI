using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s.AnalysisDto_s
{
    public class AnalysisFindingDto
    {
        public string LocationLabel { get; set; }
        public string Severity { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal Confidence { get; set; }

        public string ImagePath { get; set; }

    }
}
