using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.Attachments
{
    public interface IUrlService
    {
        string GetImageUrl(string path);
    }
}
