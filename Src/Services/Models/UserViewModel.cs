using Services.Models.Common;

namespace Services.Models
{
    public class UserDto:BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
