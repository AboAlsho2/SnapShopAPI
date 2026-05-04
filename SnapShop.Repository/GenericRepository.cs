using Microsoft.EntityFrameworkCore;
using SnapShop.Core.Models;
using SnapShop.Core.Repositories;
using SnapShop.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ShopContext _shopContext;

        public GenericRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
             return (IEnumerable<T>)await _shopContext.Products.Include(p=>p.ProductBrand).Include(p => p.ProductType).ToListAsync();

            }
            return await _shopContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _shopContext.Set<T>().FindAsync(id);
        }
    }
}
