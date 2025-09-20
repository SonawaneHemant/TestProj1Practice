using System.Security.Cryptography;
using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(AppDbContext appDbContext) : BaseAPIController
    {

        [HttpPost("registerdto")] //POST: api/account/register
        public async Task<ActionResult<AppUser>> Resister(RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();

            if (await UserEmailExists(registerDTO.Email))
            {
                return BadRequest("Email is already in use");
            }
            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            appDbContext.AppUsers.Add(user);
            await appDbContext.SaveChangesAsync();

            return user;
        }

        [HttpPost("registerWithHeader")] //POST: api/account/register
        public async Task<ActionResult<AppUser>> ResisterWithHeader(string email, string displayName, string password)
        {
            using var hmac = new HMACSHA512();
            if (await UserEmailExists(email))
            {
                //Use BadRequest when the client has added someting wrong
                return BadRequest("Email is already in use");
            }
            var user = new AppUser
            {
                DisplayName = displayName,
                Email = email.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };
            appDbContext.AppUsers.Add(user);
            await appDbContext.SaveChangesAsync();

            return user;
        }

        [HttpPost("login")] //POST: api/account/login
        public async Task<ActionResult<AppUser>> Login(LoginDTO loginDTO)
        {
           var user = await appDbContext.AppUsers.SingleOrDefaultAsync(x => x.Email.ToLower() == loginDTO.Email.ToLower());

           //use Unauthorized when the client is not authorized to access any a resource
            if (user == null) return Unauthorized("Invalid email");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDTO.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return user;
        }
        
        private async Task<bool> UserEmailExists(string email)
        {
            return await appDbContext.AppUsers.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
