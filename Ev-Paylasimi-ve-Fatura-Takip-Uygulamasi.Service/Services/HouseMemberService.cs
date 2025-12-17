using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class HouseMemberService : Service<HouseMember>, IHouseMemberService
    {
        private readonly IHouseMemberRepository _houseMemberRepository;
        public HouseMemberService(IGenericRepository<HouseMember> repository, IUnitOfWorks unitOfWorks, IHouseMemberRepository houseMemberRepository) : base(repository, unitOfWorks)
        {
            _houseMemberRepository = houseMemberRepository;
        }

        public IQueryable<House> GetHousesByUser(int userId)
        {
            return _houseMemberRepository.GetHousesByUser(userId);
        }

        public IQueryable<HouseMember> GetMembersByHouse(int houseId)
        {
            return _houseMemberRepository.GetMembersByHouse(houseId);
        }
    }
}
