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
            CreateMap<TokenDto, ApplicationUser>()
                .ForMember(y=>y.UserName,
                            x=>x.MapFrom(o=>o.UserName))
                .ForMember(y => y.RefreshToken, 
                            x => x.MapFrom(o => o.RefreshToken))
                .ForMember(y => y.RefreshTokenExpiryTime, 
                        x => x.MapFrom(o => o.RefreshTokenExpiryTime))
                .ReverseMap();
        }
    }
}