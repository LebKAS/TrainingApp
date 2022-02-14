using AutoMapper;
using DatingApp.api.Dtos;
using DatingApp.api.Models;
namespace DatingApp.api.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForListDto>()
                  .ForMember(dest=> dest.PhotoUrl,
                           opt=> { opt.MapFrom(src=> src.Photos.FirstOrDefault(p=>p.IsMain).Url ); 
                  })
                  .ForMember(dest=> dest.Age, opt =>{
                        opt.MapFrom(d=> d.DateOfBirth.CalculateAge());
                        });
            CreateMap<User,UserForDetailDto>()
                .ForMember(dest=> dest.PhotoUrl,
                           opt=> { opt.MapFrom(src=> src.Photos.FirstOrDefault(p=>p.IsMain).Url ); 
                  })
                    .ForMember(dest=> dest.Age, opt =>{
                        opt.MapFrom(d=> d.DateOfBirth.CalculateAge());
                        });
            CreateMap<Photo, PhotosForDetailDto>();
        }
    }
}