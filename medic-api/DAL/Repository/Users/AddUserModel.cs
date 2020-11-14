namespace medic_api.DAL.Repository
{
    public class AddUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}