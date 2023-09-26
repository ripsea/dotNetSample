using Services.Models.Common;

namespace Services.Models
{
    public class User:BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
