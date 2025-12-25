using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }


        [NonAction]
        public int GetUserFromToken()
        //{
        //    string requestHeader = Request.Headers["Authorization"];
        //    string jwt = requestHeader?.Replace("Bearer ", "");
        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtSecurityToken = handler.ReadToken(jwt) as JwtSecurityToken;
        //    string userId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
        //    int id = Int32.Parse(userId);
        //    return id == 0 ? 0 : id;

        //}
        {
            // Framework'ün doğruladığı User nesnesini kullandık.
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            return 0;
        }
    }
}
