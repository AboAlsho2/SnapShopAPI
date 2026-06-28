using System.ComponentModel.DataAnnotations;

namespace SnapShop.APIs.DTOs
{
    public class UserDTO
    {
        //email /displayname / token 

        [EmailAddress]
        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Token { get; set; }
    }
}
