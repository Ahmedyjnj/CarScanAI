using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Scan.BLL.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles.UrlResolver
{
    public class ContentImageResolver(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IValueResolver<User, UserProfileDto, string>
    {

      

        public string Resolve(User source, UserProfileDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ProfileImage))
            {
                return string.Empty;

            }
            else
            {
                var request = httpContextAccessor.HttpContext!.Request;

                var baseUrl = $"{request.Scheme}://{request.Host}";

                var Url = $"{baseUrl}{source.ProfileImage}";
                return Url;
            }
        }
    }
}
