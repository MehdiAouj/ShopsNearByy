using System.ComponentModel.DataAnnotations;

namespace ShopsNearByy.Models
{
    public class UserRegist
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfPassword { get; set; }
    }
}
