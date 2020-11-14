using System;
using System.Collections.Generic;
using System.Linq;
using medic_api.DAL.Models;

namespace medic_api.DAL.Repository
{
    public class UsersRepository : IUserRepository
    {
        private readonly DataContext _db;

        public UsersRepository(DataContext db)
        {
            _db = db;
        }

        public User GetUser(string id)
        {
            var entity = _db.Users.FirstOrDefault(user => user.UserId == new Guid(id));
            return entity;
        }

        public User GetUserByUserName(string userName)
        {
            var entity = _db.Users.FirstOrDefault(user => user.UserName == userName);
            return entity;
        }

        public List<User> GetUsers()
        {
            var entities = _db.Users.ToList();
            return entities;
        }

        public string AddUser(AddUserModel userModelData)
        {
            var existing = _db.Users.FirstOrDefault(u => u.UserName == userModelData.UserName);
            if (existing != null) throw new Exception("User already exists");
            if (!_roleVerify(userModelData.Role)) throw new Exception("Bad role");
            User user = new User
            {
                Password = userModelData.Password,
                Role = userModelData.Role,
                FirstName = userModelData.FirstName,
                LastName = userModelData.LastName,
                UserName = userModelData.UserName,
            };
            var newUser = _db.Users.Add(user);
            _db.SaveChanges();
            
            return newUser.Entity.UserId.ToString();
        }

        public string UpdateUser(UpdateUserModel userModelData, string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == new Guid(userId));
            if (user == null) throw new Exception("User does not exists");
            if (!_roleVerify(userModelData.Role)) throw new Exception("Bad role");
            if (!string.IsNullOrEmpty(userModelData.Password)) user.Password = userModelData.Password;
            if (!string.IsNullOrEmpty(userModelData.Role)) user.Role = userModelData.Role;
            if (!string.IsNullOrEmpty(userModelData.FirstName)) user.FirstName = userModelData.FirstName;
            if (!string.IsNullOrEmpty(userModelData.LastName)) user.LastName = userModelData.LastName;
            if (!string.IsNullOrEmpty(userModelData.UserName)) user.UserName = userModelData.UserName;
            _db.SaveChanges();
            return user.UserId.ToString();
        }

        public string DeleteUser(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == new Guid(userId));
            if (user == null) throw new Exception("User does not exists");
            _db.Users.Remove(user);
            _db.SaveChanges();
            return user.UserId.ToString();
        }

        private bool _roleVerify(string role)
        {
            return role == "Admin" || role == "Doctor" || role == "Patient";
        }
    }
}