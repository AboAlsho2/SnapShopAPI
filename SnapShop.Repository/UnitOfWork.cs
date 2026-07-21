using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnapShop.Core;
using SnapShop.Core.Models;
using SnapShop.Core.Repositories;
using SnapShop.Repository.Data;

namespace SnapShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _shopContext;
        private Hashtable Rebos;
        

        public UnitOfWork(ShopContext shopContext)
        {
            _shopContext = shopContext;
            Rebos = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
         return await _shopContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _shopContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel
        {
            if (!Rebos.ContainsKey(typeof(TEntity).Name))
            {
                var newRepo = new GenericRepository<TEntity>(_shopContext); 
                Rebos.Add(typeof(TEntity).Name, newRepo);
            }
            return (IGenericRepository<TEntity>) Rebos[typeof(TEntity).Name];

            
        }
    }
}
