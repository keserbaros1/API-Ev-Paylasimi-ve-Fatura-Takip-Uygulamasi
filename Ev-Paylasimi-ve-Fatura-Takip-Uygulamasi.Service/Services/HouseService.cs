using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class HouseService : Service<House>, IHouseService
    {
        private readonly IHouseRepository _houseRepository;
        public HouseService(IGenericRepository<House> repository, IUnitOfWorks unitOfWorks, IHouseRepository houseRepository) : base(repository, unitOfWorks)
        {
            _houseRepository = houseRepository;
        }
    }
}
