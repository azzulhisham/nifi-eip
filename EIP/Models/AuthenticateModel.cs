using System.ComponentModel.DataAnnotations;

namespace EIP.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public string TenantName { get; set; }

        public bool RememberClient { get; set; }
    }
}
