
using IKEa.DAL.Persinstance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEa.DAL.Persinstance.Repositories._Generic
{
    public class GenericRepository<T>(ApplicationDbContext Context) : IGenericRepository<T> where T : class
    {






        public IQueryable<T> GetAll(bool withnotracking = true)
        {
            if (withnotracking)
                return Context.Set<T>().AsNoTracking();

            return Context.Set<T>();
        }




        public async Task<T>? GetById(Guid id) 
        {
            var item =await Context.Set<T>().FindAsync(id); 
            return item;
        }


        public async Task AddAsync(T item)
        {
           await  Context.Set<T>().AddAsync(item);
           
        }

        public void Delete(T item)
        {
           
            Context.Set<T>().Remove(item);
           
        }

      

        public void update(T item)
        {
            Context.Set<T>().Update(item);

           
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
