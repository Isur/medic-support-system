namespace medic_api.Controllers.Auth.DTO
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }
        public string Token { get; set; }
    }
}