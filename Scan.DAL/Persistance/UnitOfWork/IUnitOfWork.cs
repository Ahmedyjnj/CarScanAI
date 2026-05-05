using IKEa.DAL.Persinstance.Repositories._Generic;
using Microsoft.AspNetCore.Identity;
using Scan.DAL.Persistance.Repositories.CarRepositories;
using Scan.DAL.Persistance.Repositories.DamageDetailRepositories;
using Scan.DAL.Persistance.Repositories.DetectionRepairCenters;
using Scan.DAL.Persistance.Repositories.Detections;
using Scan.DAL.Persistance.Repositories.RepairCenters;
using Scan.DAL.Persistance.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity:class;
         

        Task<int> SaveAsync();
    }

}
