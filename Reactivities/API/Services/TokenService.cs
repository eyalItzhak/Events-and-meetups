using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(AppUser user) //get user and create for him a token
        {
            var claims = new List<Claim> //token contain claim about the user =>this is the importent data inside our token 
            //the client sent this info for most of the rquest in our app
            //the user present this info 
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            };
            //encryption our token claims
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])); //key create
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //sign by server using the key

            var tokenDescriptor = new SecurityTokenDescriptor //discribe the token 
            {
                Subject = new ClaimsIdentity(claims), //his claims
                Expires = DateTime.Now.AddDays(7), //time to expire
                SigningCredentials = creds // the signing of the server 
            };

            var tokenHandler = new JwtSecurityTokenHandler(); //handler to make the token

            var token = tokenHandler.CreateToken(tokenDescriptor); //crate the token 

            return tokenHandler.WriteToken(token); //sent the token back
        }
    }
}