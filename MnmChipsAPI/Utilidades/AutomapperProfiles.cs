using AutoMapper;
using MnmChipsAPI.DTOs;
using MnmChipsAPI.Entidades;

namespace MnmChipsAPI.Utilidades
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Prestador, PrestadorDTO>();
            CreateMap<CrearPrestadorDTO, Prestador>().ForMember(x=>x.Foto,opciones=>opciones.Ignore());
        }
        

    }
}
