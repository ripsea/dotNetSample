using AutoMapper;
using Data.Entities;
using Data.Entities.Auth;
using Services.Models;

namespace Services.AutoMapper
{
    internal class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<ApplicationUser, TokenResultDto>()
                .ReverseMap();
        }
    }
}