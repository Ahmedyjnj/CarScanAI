using IKEa.DAL.Persinstance.Data;
using IKEa.DAL.Persinstance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using Scan.DAL.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Persistance.Repositories.CarRepositories
{
    public class CarRepository
    : GenericRepository<Car>, ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetByUserIdAsync(string userId)
        {
            return await _context.Cars
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }

}
