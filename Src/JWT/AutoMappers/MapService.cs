using AutoMapper;
using Jwt;

public class MapService: IMapService
{
    private readonly IMapper _mapper;

    //https://igouist.github.io/post/2020/07/automapper/
    //註：網路上挺多 AutoMapper 的文章會直接使用 Mapper.CreateMap() 這類靜態方法，
    //然而這些方法已經在 AutoMapper 5 的時候被廢除，改
    //成現在由 MapperConfiguration 產生 Mapper 的方法。

    //首先我們先建立一個用來放對映關係的 Profile。
    //我個人習慣會另開一個 Mappings 資料夾，並按照轉換所在的分層 + Mappings 來命名。
    //以此例, ServiceMappings為供Service功能的mapper設定

    //將 CreateMap 都整理到 Profile 之後，若是有些類別之間的轉換有對欄位做額外的處理，
    //例如 DateTime 去除時間只留下年月日，又或是某幾個欄位銜接成一個欄位等等，
    //在實際進行類別轉換的時候需要註記一下，請後續的維護人員或團隊夥伴記得先確認過 Profile，
    //避免造成一些隱藏重要資訊挖洞給人跳的問題。

    public MapService()
    {
        var config = new MapperConfiguration(
            cfg =>
                cfg.AddProfile<ServiceMappings>()
        );

        this._mapper = config.CreateMapper();
    }

}