using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class ExpenseService : Service<Expense>, IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseService(IGenericRepository<Expense> repository, IUnitOfWorks unitOfWorks, IExpenseRepository expenseRepository) : base(repository, unitOfWorks)
        {
            _expenseRepository = expenseRepository;
        }
    }
}
