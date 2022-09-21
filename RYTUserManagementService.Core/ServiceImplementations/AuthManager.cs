
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RYTUserManagementService.Core.ServiceInterfaces;
using RYTUserManagementService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using RYTUserManagementService.Dto.UserDto;

namespace RYTUserManagementService.Core.ServiceImplementations
{
    public class AuthManager
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IConfiguration config, UserManager<ApiUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public  async Task<string> CreateToken( ApiUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTSettings: TokenKey"));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512);

            var tokenOptions = new JwtSecurityToken(

                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds

              );
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        public async Task<bool> ValidateUser(LoginUserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            return (user != null && await _userManager.CheckPasswordAsync(user, userDto.Password));
        }

    

        


    }

}
