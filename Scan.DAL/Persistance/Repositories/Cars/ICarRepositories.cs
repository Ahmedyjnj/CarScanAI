using IKEa.DAL.Persinstance.Repositories._Generic;
using Scan.DAL.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.CarRepositories
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<IEnumerable<Car>> GetByUserIdAsync(string userId);
    }

}
