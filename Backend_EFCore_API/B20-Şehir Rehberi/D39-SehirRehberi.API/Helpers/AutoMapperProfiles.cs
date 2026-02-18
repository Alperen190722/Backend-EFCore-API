using AutoMapper;
using D39_SehirRehberi.API.Dtos;
using D39_SehirRehberi.API.Models;

namespace D39_SehirRehberi.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                });

            CreateMap<City, CityForDetailDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
        }
    }
}
