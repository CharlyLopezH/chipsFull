using AutoMapper;
using ChipsAPI.DTOs;
using ChipsAPI.Models;

namespace ChipsAPI.Utils
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Prestador,PrestadorDTO>().ReverseMap();
            CreateMap<PrestadorCreacionDTO, Prestador>();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserCreacionDTO, User>();

        }
    }
}
