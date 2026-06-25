using SnapShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync :BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParam Params) : base(
           p => 
           (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
           &&
           (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
            &&
            (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)

            )
        {
            
        }
    }
}
