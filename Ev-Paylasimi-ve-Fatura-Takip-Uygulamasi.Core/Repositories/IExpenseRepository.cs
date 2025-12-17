using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories
{
    public interface IExpenseRepository:IGenericRepository<Expense>
    {
        IQueryable<Expense> GetByHouse(int houseId);
    }
}
