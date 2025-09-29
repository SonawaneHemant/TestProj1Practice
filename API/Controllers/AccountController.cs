using System.Security.Cryptography;
using API.Data;
using API.DTO;
using API.Entities;
using API.Extentions;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Controllers
{
    public class AccountController(AppDbContext appDbContext,ITokenService tokenService) : BaseAPIController
    {

        [HttpPost("registerdto")] //POST: api/account/register
        public async Task<ActionResult<UserDTo>> Resister(RegisterDTO registerDTO)
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

            // var userDto = new UserDTo
            // {
            //     Id = user.ID,
            //     Email = user.Email,
            //     DisplayName = user.DisplayName,
            //     Token = tokenService.CreateToken(user) 
            // };

            // return new UserDTo by using extension method
            //var userDto = API.Extentions.AppSuerExtentions.ToDto(user, tokenService);
            //return userDto;

            //we can also return like this so it will be user Extention means no need to pass use parameter in call of method.
            return user.ToDto(tokenService);
        }

        // [HttpPost("registerWithHeader")] //POST: api/account/register
        // public async Task<ActionResult<AppUser>> ResisterWithHeader(string email, string displayName, string password)
        // {
        //     using var hmac = new HMACSHA512();
        //     if (await UserEmailExists(email))
        //     {
        //         //Use BadRequest when the client has added someting wrong
        //         return BadRequest("Email is already in use");
        //     }
        //     var user = new AppUser
        //     {
        //         DisplayName = displayName,
        //         Email = email.ToLower(),
        //         PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
        //         PasswordSalt = hmac.Key
        //     };
        //     appDbContext.AppUsers.Add(user);
        //     await appDbContext.SaveChangesAsync();

        //     return user;
        // }

        [HttpPost("login")] //POST: api/account/loginA
        public async Task<ActionResult<UserDTo>> Login(LoginDTO loginDTO)
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

            // var userDto = new UserDTo
            // {
            //     Id = user.ID,
            //     Email = user.Email,
            //     DisplayName = user.DisplayName,
            //     Token = tokenService.CreateToken(user)
            // };

            // return userDto;
            
            return user.ToDto(tokenService);
        }
        
        private async Task<bool> UserEmailExists(string email)
        {
            return await appDbContext.AppUsers.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
