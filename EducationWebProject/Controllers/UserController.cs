using EducationWebProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace EducationWebProject.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthenticationContext _authenticationContext;
        public UserController(AuthenticationContext authenticationContext)
        {
            _authenticationContext = authenticationContext;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterRequest request)
        {
            if (_authenticationContext.Users.Any(u => u.Email == request.Email)) // check if user already exists
            {
                return BadRequest("User already exists.");

            }
            CreatePasswordHash(request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            _authenticationContext.Users.Add(user);
            await _authenticationContext.SaveChangesAsync();

            return Ok("User successfully created!");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginRequest request)
        {
            var user=await _authenticationContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if(!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect.");
            }

            return Ok($"Welcome back, {user.Email}!");
        }
        private void CreatePasswordHash(string passsword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
            }
        }
        private bool VerifyPasswordHash(string passsword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
