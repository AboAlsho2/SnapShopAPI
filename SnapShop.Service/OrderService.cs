using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnapShop.Core.Models;
using SnapShop.Core.Models.Order;
using SnapShop.Core.Services;
using SnapShop.Repository;
using SnapShop.Repository.Data.Configurations;


namespace SnapShop.Service
{
    internal class OrderService : IOrderService
    {
        private readonly BasketRepository _basketRepository;
        private readonly GenericRepository<Product> _productRepo;
        private readonly GenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly GenericRepository<Order> _orderRepo;

        public OrderService(BasketRepository basketRepository, GenericRepository<Product> productRepo, GenericRepository<DeliveryMethod> deliveryMethodRepo, GenericRepository<Order> orderRepo)
        {
            _basketRepository = basketRepository;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _deliveryMethodRepo = deliveryMethodRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deleveryMethodId, Address address)
        {
            var basket =await _basketRepository.GetBasketAsync(basketId);
            var items = new List<OrderItem>();

            if (basket?.Items.Count > 0)
            {
                foreach (var basketItem in basket.Items)
                {
                    var product = await _productRepo.GetByIdAsync(basketItem.Id);
                    var productItemOrdered = new ProductItemOrder(product.Id, product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, basketItem.Price, basketItem.Quantity);
                    items.Add(orderItem);

                }
            }
            
            decimal subTotal = 0;
            foreach (var item in items) 
            {
                subTotal += item.Price * item.Quantity;
            }

            var deliveryMethod = await _deliveryMethodRepo.GetByIdAsync(deleveryMethodId);
            
            Order order = new Order(buyerEmail,address, deliveryMethod,items,subTotal);

            await _orderRepo.AddAsync(order);

            return order;
        }

        public Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
