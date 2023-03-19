using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserUsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserPasswordMaxLength)]
        public string Password { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserNamelMaxLength)]
        public string Name { get; set; }

        public decimal Budget { get; set; }
    }
}
