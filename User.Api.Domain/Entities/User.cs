using System.ComponentModel.DataAnnotations;
using User.Api.Domain.Common;

namespace User.Api.Domain.Entities
{
    public class UserEntity : SqlBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(70)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
