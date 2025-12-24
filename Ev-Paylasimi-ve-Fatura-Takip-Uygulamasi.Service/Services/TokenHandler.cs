using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration Configuration;

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string CreateRefrestToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        public Token CreateToken(User user)
        //public Token CreateToken(User user, List<HouseMember> roles)
        {
            Token token = new Token();

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.Now.AddDays(7);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: token.Expiration,
                claims: SetClaims(user),
                //claims: SetClaims(user, roles),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            token.RefrestToken = CreateRefrestToken();

            return token;
        }

        public IEnumerable<Claim> SetClaims(User user)
        //public IEnumerable<Claim> SetClaims(User user, List<HouseMember> roles)
        {
            //Claim claim = new("sub",user.Id.ToString());
            List<Claim> claims = new List<Claim>();

            //claims.Add(claim);
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName(user.Name);
            claims.AddEmail(user.Email);
            //claims.AddRoles(roles.Select( p => p.Role).ToArray());

            return claims;
        }
    }
}
