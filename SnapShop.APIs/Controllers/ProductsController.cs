using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnapShop.APIs.DTOs;
using SnapShop.APIs.Errors;
using SnapShop.APIs.Helpers;
using SnapShop.Core.Models;
using SnapShop.Core.Repositories;
using SnapShop.Core.Specifications;

namespace SnapShop.APIs.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductType> typeRepo ,IGenericRepository<ProductBrand> brandRepo , IMapper mapper)
        {
            _productRepo = productRepo;
            _typeRepo = typeRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductsToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Pagination<ProductsToReturnDTO>>> GetAllProducts([FromQuery]ProductSpecParam  Params )
        {
            var specs = new ProductWithBrandAndTypeSpecs(Params);
            var products = await _productRepo.GetAllWithSpecsAsync(specs);
            if (products == null) return NotFound( new ApiResponse(StatusCodes.Status404NotFound));
            var count = await _productRepo.GetProductCountWithSpecsAsync(new ProductWithFiltrationForCountAsync(Params));
            var mappedProduct = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductsToReturnDTO>>(products);

            var returenedProducts = new Pagination<ProductsToReturnDTO>
            {
                PageIndex = Params.PageIndex,
                PageSize = Params.PageSize,
                Data = mappedProduct,
                Count = count

            };
            
            return Ok(returenedProducts);

        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductsToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductsToReturnDTO>> GetProductByID(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecs(id);
           
            var product = await _productRepo.GetByIdWithSpecsAsync(id ,specs);
           // product.ToString();
            if (product == null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            var mappedProduct = _mapper.Map<Product, ProductsToReturnDTO>(product);

            return Ok(mappedProduct); 

        }

        [HttpGet("Brands")]
        [ProducesResponseType(typeof(ProductBrand), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            if (brands == null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return Ok(brands);

        }

        [HttpGet("Types")]
        [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            if (Types == null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return Ok(Types);
            
        }
    }
}
