using AutoMapper;
using SnapShop.APIs.DTOs;
using SnapShop.Core.Models;

namespace SnapShop.APIs.Helpers
{
    public class MappingProfiles :Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product,ProductsToReturnDTO>()
                .ForMember(d=>d.ProductBrand, o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType, o=>o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl, o=>o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
