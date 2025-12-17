using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Repositories
{
    public class PaymentRepository: GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public IQueryable<Payment> GetPaymentsByHouse(int houseId)
        {
            return _context.Payments.Where(p => p.HouseId == houseId);
        }

        public IQueryable<Payment> GetPaymentsByUser(int userId)
        {
            return _context.Payments.Where(p => p.UserId == userId);
        }
    }
}
