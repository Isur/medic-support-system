using System.ComponentModel.DataAnnotations;

namespace medic_api.Controllers.Users
{
    public class UpdateUserRequest
    {
        public string? UserName { get; set; }
        public string? Firstname { get; set; }
        public string? LastName { get; set; }
        [MinLength(4), MaxLength(16)]
        public string? Password { get; set; }
    }
}