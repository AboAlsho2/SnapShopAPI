using SnapShop.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Services
{
    public interface IOrderService
    {

        public Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deleveryMethodId , Address address);
        public Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string buyerEmail);
        public Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId);

    }
}
