using Microsoft.AspNetCore.Http;
using Scan.BLL.Dto_s.CarDto_s;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s.AnalysisDto_s
{
    public class AnalyzeRequestNewDto
    {
        
        public IFormFile Image { get; set; }

       // public string LocationLabel { get; set; }

        public CreateCarDto CarDto { get; set; }


    }
    public class AnalyzeRequestDto
    {

        public IFormFile Image { get; set; }

      //  public string LocationLabel { get; set; }

        public Guid CarId { get; set; }

    }
}
