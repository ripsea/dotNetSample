using Services.Models.Common;

namespace Services.Models
{
    public class Tokens:BaseModel
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
