using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SnapShop.Core.Models.Identity;
using SnapShop.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser user , UserManager<AppUser> userManager)
        {

            //Payload 

            //PrivateClaims (user Defined ) 

            var authClaims = new List<Claim>()
            { 
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            { 
            authClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims : authClaims,
                signingCredentials: new SigningCredentials( authKey , SecurityAlgorithms.HmacSha256)
                );

            var returnedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return returnedToken;
        }
    }
}
