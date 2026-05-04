using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnapShop.Core.Models;
using SnapShop.Core.Repositories;

namespace SnapShop.APIs.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {

            var products = await _productRepo.GetAllAsync();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int id)
        {
            var products = await _productRepo.GetByIdAsync(id);
            return Ok(products);

        }
    }
}
