using AutoMapper;
using Data.Entities.Auth;
using Jwt.Models;
using JWT.Models;
using Services.Models;

public class ServiceMappings : Profile
{
    public ServiceMappings()
    {
        CreateMap<TokenDto, LoginRequest>()
            //它能夠反轉對映，建立一個反方向的對映表。
            //當你的類別之間可能需要往回轉型，又或是想要忽略來源類別的欄位時，
            //ReverseMap 就會顯得相當有用
            .ReverseMap();

        // ...其他的對映內容 (使用 CreateMap<> 建立下一組)
    }
}