
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEa.DAL.Persinstance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : class
    {



        IQueryable<T> GetAll(bool withnotracking = true);

        Task<T>? GetById(Guid id);

        Task AddAsync(T entity);

        void update(T entity);

        void Delete(T entity);


        Task AddRangeAsync(IEnumerable<T> entities);
         void DeleteRange(IEnumerable<T> entities);




    }
}
