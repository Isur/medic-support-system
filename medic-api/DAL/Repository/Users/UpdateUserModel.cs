namespace medic_api.DAL.Repository
{
    public class UpdateUserModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
    }
}