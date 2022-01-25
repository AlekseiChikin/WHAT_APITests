using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("fisrtName")]
        public string UserFirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string UserLastName { get; set; }
        [JsonPropertyName("role")]
        public int UserRole { get; set; }
        [JsonPropertyName("roleList")]
        public Dictionary<string, string> RoleAndToken { get; set; }
 
    }
 
}
