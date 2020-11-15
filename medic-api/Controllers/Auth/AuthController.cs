using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using medic_api.Controllers.Auth.DTO;
using medic_api.DAL.Repository;
using medic_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace medic_api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest body)
        {
            var user = _userRepository.GetUserByUserName(body.UserName);
            if (user == null) return Unauthorized("Wrong username or password!");
            if(!PasswordHasher.Verify(body.Password, user.Password)) return Unauthorized("Wrong username or password!");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Security:SecretKey")));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                },
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signingCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            LoginResponse response = new LoginResponse()
            {
                Firstname = user.FirstName,
                Id = user.UserId.ToString(),
                Role = user.Role,
                LastName = user.LastName,
                UserName = user.UserName,
                Token = tokenString,
            };
            return Ok(response);
        }

        [HttpPost, Route("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest body)
        {
            AddUserModel model = new AddUserModel()
            {
                Role = "Patient",
                FirstName = body.Firstname,
                LastName = body.LastName,
                UserName = body.UserName,
                Password = PasswordHasher.Hash(body.Password),
            };
            var userId = _userRepository.AddUser(model);
            RegisterResponse response = new RegisterResponse()
            {
                Firstname = model.FirstName,
                Id = userId,
                Role = model.Role,
                LastName = model.LastName,
                UserName = model.UserName,
            };

            return Ok(response);
        }
    }
}