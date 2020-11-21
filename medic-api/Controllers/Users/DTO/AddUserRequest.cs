using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace medic_api.Controllers.Users
{
    public class AddUserRequest
    {
        [JsonRequired]
        public string Role { get; set; }
        [JsonRequired]
        public string UserName { get; set; }
        [JsonRequired]
        public string Firstname { get; set; }
        [JsonRequired]
        public string LastName { get; set; }
        [JsonRequired, MinLength(4), MaxLength(16)]
        public string Password { get; set; }
    }
}