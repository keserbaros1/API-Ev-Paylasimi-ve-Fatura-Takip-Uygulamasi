using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Repositories
{
    public class HouseMemberRepository : GenericRepository<HouseMember>, IHouseMemberRepository
    {
        private readonly AppDbContext _context;

        public HouseMemberRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<House> GetHousesByUser(int userId)
        {
            return _context.HouseMembers
                .Where(hm => hm.UserId == userId)
                .Select(hm => hm.House);
        }

        public IQueryable<HouseMember> GetMembersByHouse(int houseId)
        {
            return _context.HouseMembers
                .Where(hm => hm.HouseId == houseId);
        }

        public async Task<string> GetUserRoleInHouseAsync(int userId, int houseId)
        {
            var member = await Where(hm => hm.UserId == userId && hm.HouseId == houseId)
                .FirstOrDefaultAsync();

            return member?.Role;
        }

        public async Task<bool> IsUserAdminOfHouseAsync(int userId, int houseId)
        {
            return await AnyAsync(hm => hm.UserId == userId && hm.HouseId == houseId && hm.Role == "Admin");
        }

        public async Task<bool> IsUserMemberOfHouseAsync(int userId, int houseId)
        {
            return await AnyAsync(hm => hm.UserId == userId && hm.HouseId == houseId);
        }
    }

}
