using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    // ..\Ev-Paylasimi-ve-Fatura-Takip-Uygulamasi.Service\Services\PaymentService.cs

    public class PaymentService : Service<Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository; // Changed from IPaymentService

        // Inject IPaymentRepository instead of IPaymentService
        public PaymentService(IGenericRepository<Payment> repository, IUnitOfWorks unitOfWorks, IPaymentRepository paymentRepository) : base(repository, unitOfWorks)
        {
            _paymentRepository = paymentRepository;
        }

        public IQueryable<Payment> GetPaymentsByHouse(int houseId)
        {
            return _paymentRepository.GetPaymentsByHouse(houseId); // Use repository
        }

        public IQueryable<Payment> GetPaymentsByUser(int userId)
        {
            return _paymentRepository.GetPaymentsByUser(userId); // Use repository
        }
    }
}
