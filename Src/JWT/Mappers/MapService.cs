using AutoMapper;
using Jwt;

public class MapService: IMapService
{
    private readonly IMapper _mapper;

    public MapService()
    {
        var config = new MapperConfiguration(
            cfg =>
                cfg.AddProfile<ServiceMappings>()
        );

        this._mapper = config.CreateMapper();
    }

}