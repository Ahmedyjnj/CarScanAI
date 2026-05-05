using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEa.DAL.Persinstance.Data;
using IKEa.DAL.Persinstance.Repositories._Generic;
using Scan.DAL.Persistance.Repositories.CarRepositories;
using Scan.DAL.Persistance.Repositories.DamageDetailRepositories;
using Scan.DAL.Persistance.Repositories.DetectionRepairCenters;
using Scan.DAL.Persistance.Repositories.Detections;
using Scan.DAL.Persistance.Repositories.RepairCenters;
using Scan.DAL.Persistance.Repositories.Users;


namespace Scan.DAL.Persistance.UnitOfWork
{
    
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        

   

        private readonly Dictionary<string, object> _Repositories = new Dictionary<string, object>();

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var TypeName = typeof(TEntity).Name;

            if (_Repositories.ContainsKey(TypeName))
                return (IGenericRepository<TEntity>)_Repositories[TypeName];

            var Repo= new GenericRepository<TEntity>(context);
            _Repositories.Add(TypeName, Repo);
            return Repo;


        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        
    }

}
