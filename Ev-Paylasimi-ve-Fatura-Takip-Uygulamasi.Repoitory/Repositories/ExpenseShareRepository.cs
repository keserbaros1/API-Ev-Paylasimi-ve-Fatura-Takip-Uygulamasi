using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Repositories
{
    public class ExpenseShareRepository: GenericRepository<ExpenseShare>, IExpenseShareRepository
    {
        private readonly AppDbContext _context;

        public ExpenseShareRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public IQueryable<ExpenseShare> GetExpenseSharesByUser(int userId)
        {
            return _context.ExpenseShares.Where(es => es.UserId == userId);
        }
    }
}
