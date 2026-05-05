using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s.CarDto_s
{
    public class CreateCarDto
    {
        [Required(ErrorMessage = "Plate number is required.")]
        [StringLength(32,
            ErrorMessage =
            "Plate number must not exceed 32 characters.")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(128,
            ErrorMessage =
            "Brand must not exceed 128 characters.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(128,
            ErrorMessage =
            "Model must not exceed 128 characters.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2100,
            ErrorMessage =
            "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(64,
            ErrorMessage =
            "Color must not exceed 64 characters.")]
        public string Color { get; set; }
    }
}
