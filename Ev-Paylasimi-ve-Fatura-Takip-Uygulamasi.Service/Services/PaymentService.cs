using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class PaymentService : Service<Payment>, IPaymentService
    {
        private readonly IPaymentService _paymentService;
        public PaymentService(IGenericRepository<Payment> repository, IUnitOfWorks unitOfWorks, IPaymentService paymentService) : base(repository, unitOfWorks)
        {
            _paymentService = paymentService;
        }

        public IQueryable<Payment> GetPaymentsByHouse(int houseId)
        {
            return _paymentService.GetPaymentsByHouse(houseId);
        }

        public IQueryable<Payment> GetPaymentsByUser(int userId)
        {
            return _paymentService.GetPaymentsByUser(userId);
        }
    }
}
