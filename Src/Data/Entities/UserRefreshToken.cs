using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("UserRefreshToken")]
    public class UserRefreshToken: EntityBase
    {
        [Required]
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;

        //[ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}
