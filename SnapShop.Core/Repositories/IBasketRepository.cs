using SnapShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Repositories
{
    public interface IBasketRepository
    {
         Task<CustomerBasket?> GetBasketAsync(string id);
         Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
         Task<bool> DeleteBasketAsync(string id);
    }
}
