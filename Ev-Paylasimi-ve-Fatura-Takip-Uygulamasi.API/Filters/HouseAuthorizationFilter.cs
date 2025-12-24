using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Filters
{
    public class HouseAuthorizationFilter : IAsyncActionFilter
    {
        private readonly IHouseMemberService _houseMemberService;
        private readonly bool _requireAdmin;

        public HouseAuthorizationFilter(IHouseMemberService houseMemberService, bool requireAdmin = false)
        {
            _houseMemberService = houseMemberService;
            _requireAdmin = requireAdmin;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new UnauthorizedObjectResult(
                    CustomResponseDto<NoContentDto>.Fail(401, "Kullanıcı kimliği bulunamadı."));
                return;
            }

            // houseId parametresini al (route'dan veya body'den)
            var houseIdValue = context.ActionArguments.ContainsKey("houseId")
                ? context.ActionArguments["houseId"]
                : null;

            if (houseIdValue == null)
            {
                await next();
                return;
            }

            var houseId = (int)houseIdValue;
            var userIdInt = int.Parse(userId);

            // Yetki kontrolü
            bool hasAccess = _requireAdmin
                ? await _houseMemberService.IsUserAdminOfHouseAsync(userIdInt, houseId)
                : await _houseMemberService.IsUserMemberOfHouseAsync(userIdInt, houseId);

            if (!hasAccess)
            {
                var message = _requireAdmin
                    ? "Bu işlem için admin yetkisi gereklidir."
                    : "Bu eve erişim yetkiniz bulunmamaktadır.";

                context.Result = new ObjectResult(CustomResponseDto<NoContentDto>.Fail(403, message))
                {
                    StatusCode = 403
                };
                return;
            }

            await next();
        }
    }
}