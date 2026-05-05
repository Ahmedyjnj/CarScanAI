using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s
{
 
    /// dTO for returning user profile data .
   
    public class UserProfileDto
    {
       
            public string Id { get; set; } = null!;
            public string UserName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string? PhoneNumber { get; set; }
            public string Name { get; set; } = null!;
            public string? ProfileImage { get; set; }
            public string Status { get; set; } = "Active";

            public DateTime CreatedAt { get; set; }
        

    }
}
