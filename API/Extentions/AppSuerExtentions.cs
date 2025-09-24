using System;
using API.DTO;
using API.Entities;
using API.Interfaces;

namespace API.Extentions;

public static class AppSuerExtentions
{
    //this is extension method for Appuser where by using this keyward we are extending the AppUser class.
    //we are passing ITokenService in constructor becaz we cant craete the instance of ITokenService in this class like Dependency injection.
    //so we have to pass the ITokenService from the calling method.
    public static UserDTo ToDto(this AppUser user, ITokenService tokenService)
    {
        var userDto = new UserDTo
        {
            Id = user.ID,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
        return userDto;
    }
    
}
