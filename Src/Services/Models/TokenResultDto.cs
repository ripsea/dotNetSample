using Services.Models.Common;

namespace Services.Models
{
    public class TokenResultDto:TokenDto
    {
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }

    }
}
