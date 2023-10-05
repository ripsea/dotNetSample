using AutoMapper;
using Data.Entities;
using Services.Models;

namespace Services.AutoMapper
{
    internal class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<UserRefreshToken, TokenDto>()
                .ReverseMap();

            // All other mappings goes here
        }
    }
}