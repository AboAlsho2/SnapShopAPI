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


        }
        public ProductWithBrandAndTypeSpecs(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

        }

    }
}
