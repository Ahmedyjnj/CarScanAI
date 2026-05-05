using AutoMapper;
using Scan.BLL.Dto_s;
using Scan.BLL.Dto_s.AnalysisDto_s;
using Scan.BLL.Dto_s.CarDto_s;
using Scan.DAL.Models.Car;
using Services.MappingProfiles.UrlResolver;
using Shared.Dto_s.IdentityDto_s;

namespace Scan.BLL.Mappings
{
  
    public class CarScanAiMappingProfile : Profile
    {
        public CarScanAiMappingProfile()
        {
           
            CreateMap<Car, CarDto>();

            CreateMap<Car, CreateCarDto>().ReverseMap(); 

            CreateMap<CarDto, Car>()
                .ForMember(d => d.CarId, o => o.Ignore())
                .ForMember(d => d.UserId, o => o.Ignore())
                .ForMember(d => d.User, o => o.Ignore())
                .ForMember(d => d.Detections, o => o.Ignore());




            //CreateMap<User, UserProfileDto>().ReverseMap()
            //    .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom<ContentImageResolver>());

            CreateMap<User, UserProfileDto>()
             .ForMember(dest => dest.ProfileImage,
                opt => opt.MapFrom<ContentImageResolver>())
                    .ReverseMap();


            CreateMap<User, UserSimpleDto>().ReverseMap();


           
            CreateMap<RegisterDto, User>();



            CreateMap<UpdateProfileDto, User>();

            CreateMap<RepairCenter, RepairCenterRecommendationDto>();

            CreateMap<RepairCenterRecommendationDto,RepairCenter>().ReverseMap();

        }
    }
}
