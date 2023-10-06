using AutoMapper;
using Jwt.Models;
using JWT.Models;
using Services.Models;

public class ServiceMappings : Profile
{
    public ServiceMappings()
    {
        CreateMap<UserDto, LoginViewModel>()
            .ReverseMap();

        // ...其他的對映內容 (使用 CreateMap<> 建立下一組)
    }
}