using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories
{
    public interface IPaymentRepository:IGenericRepository<Payment>
    {
        IQueryable<Payment> GetPaymentsByHouse(int houseId);
        IQueryable<Payment> GetPaymentsByUser(int userId);

    }
}
