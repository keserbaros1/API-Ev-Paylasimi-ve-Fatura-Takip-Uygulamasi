using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services
{
    public interface IService<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        int Count();
        void Update(T entity);
        void ChangeStatus(T entity);
        Task AddAsync(T entity);
    }
}
