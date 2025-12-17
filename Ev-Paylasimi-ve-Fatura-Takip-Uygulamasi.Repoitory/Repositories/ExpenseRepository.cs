using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Repositories
{
    public class ExpenseRepository: GenericRepository<Expense>, IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Expense> GetByHouse(int houseId)
        {
            return _context.Expenses.Where(e => e.HouseId == houseId);
        }
    }
}
