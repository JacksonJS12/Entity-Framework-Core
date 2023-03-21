using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserUsernameMaxLength)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.UserPasswordMaxLength)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.UserNamelMaxLength)]
        public string Name { get; set; } = null!;

        public decimal Budget { get; set; }

        public virtual ICollection<Bet> Bets{ get; set; }
    }
}
