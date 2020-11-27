using medic_api.Controllers.Users;
using medic_api.DAL.Models;

namespace medic_api.Helpers
{
    public static class UserModelToResponse
    {
        public static UserResponse UserToResponse(User user)
        {
            var dataEncryptor = new RSA();
            return new UserResponse()
            {
                Role = user.Role,
                FirstName = dataEncryptor.Decrypt(user.FirstName),
                LastName = dataEncryptor.Decrypt(user.LastName),
                UserId = user.UserId.ToString(),
                UserName = user.UserName,
            };
        }
    }
}