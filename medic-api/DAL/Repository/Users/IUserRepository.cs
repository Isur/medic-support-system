using System.Collections.Generic;
using medic_api.DAL.Models;

namespace medic_api.DAL.Repository
{
    public interface IUserRepository
    {
        public User GetUser(string id);
        public User GetUserByUserName(string userName);
        public List<User> GetUsers();
        public string AddUser(AddUserModel userModelData);
        public string UpdateUser(UpdateUserModel userModelData, string userId);
        public string DeleteUser(string userId);
    }
}