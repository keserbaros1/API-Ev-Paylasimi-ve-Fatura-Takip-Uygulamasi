using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class ExpenseShareService : Service<ExpenseShare>, IExpenseShareService
    {
        private readonly IExpenseShareRepository _expenseShareRepository;
        public ExpenseShareService(IGenericRepository<ExpenseShare> repository, IUnitOfWorks unitOfWorks, IExpenseShareRepository expenseShareRepository) : base(repository, unitOfWorks)
        {
            _expenseShareRepository = expenseShareRepository;
        }
    }
}
