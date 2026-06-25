using SnapShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecs : BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecs(ProductSpecParam Params) : base(
           p =>
           (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
           &&
           (!Params.TypeId.HasValue||p.ProductTypeId == Params.TypeId)
            &&
            (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)

            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort) {
                    case "PriceAsc":
                        OrderAscending(p => p.Price); 
                        break;
                    case "PriceDes":
                        OrderDescending(p => p.Price);
                        break;
                    default:
                        OrderAscending(p => p.Name);
                        break;

                }
            }

            ApplyPagination(Params.pageSize, (Params.PageIndex - 1) * Params.pageSize);

        }
        public ProductWithBrandAndTypeSpecs(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

        }

    }
}
