using Microsoft.AspNetCore.StaticAssets;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Extensions
{
    public static class ClaimsExtensions
    {
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string userId)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
        }

        //public static void AddRoles(this ICollection<Claim> claims, string[] roles) 
        //{
        //    roles.ToList().ForEach(role =>
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    });
        //}
    }
}
