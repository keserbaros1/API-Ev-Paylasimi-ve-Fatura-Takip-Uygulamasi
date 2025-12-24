using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories
{
    public interface IHouseMemberRepository:IGenericRepository<HouseMember>
    {
        IQueryable<House> GetHousesByUser(int userId);
        IQueryable<HouseMember> GetMembersByHouse(int houseId);
        Task<bool> IsUserMemberOfHouseAsync(int userId, int houseId);
        Task<bool> IsUserAdminOfHouseAsync(int userId, int houseId);
        Task<string> GetUserRoleInHouseAsync(int userId, int houseId);
    }
}
