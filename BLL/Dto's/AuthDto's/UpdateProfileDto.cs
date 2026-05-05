using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s
{
    /// <summary>
    /// DTO for updating user profile (write).
    /// </summary>
    public class UpdateProfileDto
    {
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string? Name { get; set; }

        [StringLength(256, ErrorMessage = "Profile image path must not exceed 256 characters.")]
        public string? ProfileImage { get; set; }

        [StringLength(32, ErrorMessage = "Status must not exceed 32 characters.")]
        public string? Status { get; set; }
    }
}
