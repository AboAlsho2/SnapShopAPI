using System.ComponentModel.DataAnnotations;

namespace SnapShop.APIs.DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
