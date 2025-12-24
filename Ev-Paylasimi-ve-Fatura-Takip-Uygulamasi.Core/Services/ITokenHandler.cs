using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services
{
    public interface ITokenHandler
    {
        //Token CreateToken(User user, List<HouseMember> roles);
        Token CreateToken(User user);
        string CreateRefrestToken();
        IEnumerable<Claim> SetClaims(User user);
        //IEnumerable<Claim> SetClaims(User user, List<HouseMember> roles);
    }
}
