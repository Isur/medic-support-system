using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using medic_api.DAL;
using medic_api.DAL.Models;
using medic_api.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        [Authorize(Policy = "Patient")]
        [Route("me")]
        public ActionResult<UserResponse> GetProfile()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            var user = _userRepository.GetUser(userId);
            var response = Helpers.UserModelToResponse.UserToResponse(user);
            return Ok(response);
        }
        
        [HttpPatch]
        [Authorize(Policy = "Patient")]
        [Route("me")]
        public ActionResult<string> PatchProfile([FromBody] UpdateUserModel body)
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            var updateUser = new UpdateUserModel()
            {
                Password = Helpers.PasswordHasher.Hash(body.Password),
                Role = body.Role,
                FirstName = body.FirstName,
                LastName = body.LastName,
                UserName = body.UserName,
            };
            var user = _userRepository.UpdateUser(updateUser, userId);
            return Ok(user);
        }
        
        [HttpGet]
        [Authorize(Policy = "Doctor")]
        public ActionResult<List<UserResponse>> Get()
        {
            var users = _userRepository.GetUsers();
            var userList = new List<UserResponse>();
            foreach (var user in users)
            {
                var newResponse = Helpers.UserModelToResponse.UserToResponse(user);
                userList.Add(newResponse);
            }
            return Ok(userList);
        }

        [HttpGet]
        [Authorize(Policy = "Doctor")]
        [Route("{id}")]
        public ActionResult<UserResponse> Get(string id)
        {
            var user = _userRepository.GetUser(id);
            var response = Helpers.UserModelToResponse.UserToResponse(user);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "Doctor")]
        public ActionResult<string> Post([FromBody] AddUserRequest body)
        {
            if (body.Role == "Doctor" || body.Role == "Admin")
            {
                var role = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == "Doctor") return BadRequest("Only Admin can add new doctor or admin");
            }
            var newUser = new AddUserModel()
            {
                Password = Helpers.PasswordHasher.Hash(body.Password),
                Role = body.Role,
                FirstName = body.Firstname,
                LastName = body.LastName,
                UserName = body.UserName,
            };
            var user = _userRepository.AddUser(newUser);
            return Ok(user);
        }
        
        [HttpPatch]
        [Authorize(Policy = "Admin")]
        [Route("{id}")]
        public ActionResult<string> Patch(string id, [FromBody] UpdateUserModel body)
        {
            var updateUser = new UpdateUserModel()
            {
                Password = Helpers.PasswordHasher.Hash(body.Password),
                Role = body.Role,
                FirstName = body.FirstName,
                LastName = body.LastName,
                UserName = body.UserName,
            };
            var user = _userRepository.UpdateUser(updateUser, id);
            return Ok(user);
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        [Route("{id}")]
        public ActionResult<string> Delete(string id)
        {
            var user = _userRepository.DeleteUser(id);
            return Ok(user);
        }
    }
}