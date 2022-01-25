using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public class Authentication
    {
        [JsonPropertyName("email")]
        public string UserEmail { get; set; }

        [JsonPropertyName("password")]
        public string UserPassword { get; set; }
    }

}
