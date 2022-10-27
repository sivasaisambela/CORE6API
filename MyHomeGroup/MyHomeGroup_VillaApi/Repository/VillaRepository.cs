using Microsoft.EntityFrameworkCore;
using MyHomeGroup_VillaApi.Data;
using MyHomeGroup_VillaApi.Models;
using MyHomeGroup_VillaApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyHomeGroup_VillaApi.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Villa entity)
        {
            await  _dbContext.Villas.AddAsync(entity);
             await Save();
        }

        public async Task UpdateAsync(Villa entity)
        {
             _dbContext.Villas.Update(entity);
            await Save();
        }

        public async Task<Villa> GetVillaAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _dbContext.Villas.Include(x => x.Amenties); ;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter!=null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetVillasAsync(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> query = _dbContext.Villas.Include(x=>x.Amenties);

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            _dbContext.Remove(entity);
            await Save();
        }

        public async  Task Save()
        {
           await  _dbContext.SaveChangesAsync();
        }
    }
}
