using SnapShop.Core.Models;
using SnapShop.Core.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel 
    {
        #region Without Specs
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion

        #region With Specs 
        Task<IEnumerable<T>> GetAllWithSpecsAsync(ISpecifications<T> Specs);
        Task<T> GetByIdWithSpecsAsync(int id, ISpecifications<T> Specs);

        Task<int> GetProductCountWithSpecsAsync(ISpecifications<T> Specs);

        #endregion

    }
}
