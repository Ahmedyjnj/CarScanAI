using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Dto_s
{
    public class UserSimpleDto
    {
        public string Email { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
