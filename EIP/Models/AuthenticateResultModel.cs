using System.Collections.Generic;

namespace EIP.Models
{
    public class AuthenticateResultModel
    {
        public AuthenticateResultModel()
        {
            Permissions = new List<string>();
            //Personalizations = new List<ResponsePersonalizationDto>();
        }
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public long UserId { get; set; }

        public int? TenantId { get; set; }
        public string Username { get; set; }
        public string TenantName { get; set; }
        public string TenancyName { get; set; }
        public List<string> Permissions { get; set; }
        public string UserLevel { get; set; }
        public string ENCToken { get; set; }
        //public List<ResponsePersonalizationDto> Personalizations { get; set; }

    }
}
