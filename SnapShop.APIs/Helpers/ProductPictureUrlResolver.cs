using AutoMapper;
using SnapShop.APIs.DTOs;
using SnapShop.Core.Models;

namespace SnapShop.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductsToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source,
            ProductsToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }
            else return string.Empty ;
        }
    }
}
