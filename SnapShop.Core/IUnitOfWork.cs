using SnapShop.Core.Models;
using SnapShop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync();

        IGenericRepository<TEntity> Repository <TEntity>() where TEntity : BaseModel;
    }
}
