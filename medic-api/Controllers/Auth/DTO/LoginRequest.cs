using System.ComponentModel.DataAnnotations;

namespace medic_api.Controllers.Auth.DTO
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}