using MyHomeGroup_VillaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyHomeGroup_VillaApi.Repository.IRepository
{
    public interface IVillaRepository
    {

        Task<List<Villa>> GetVillasAsync(Expression<Func<Villa,bool>> filter = null);

        Task<Villa> GetVillaAsync(Expression<Func<Villa, bool>> filter = null,bool tracked=true);

        Task CreateAsync(Villa entity);

        Task UpdateAsync(Villa entity);

        Task RemoveAsync(Villa entity);

        Task Save();

    }
}
