using System.ComponentModel.DataAnnotations;

namespace medic_api.Controllers.Auth.DTO
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, MinLength(4), MaxLength(16)]
        public string Password { get; set; }
    }
}