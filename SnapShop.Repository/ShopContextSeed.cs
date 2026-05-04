using SnapShop.Core.Models;
using SnapShop.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SnapShop.Repository
{
    public   static class ShopContextSeed
    {

        public async static Task SeedAsync(ShopContext shopContext)
        {

            if (!shopContext.Brands.Any())
            {

                var BrandsData = File.ReadAllText("../SnapShop.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var brand in Brands)
                    {
                        await shopContext.Set<ProductBrand>().AddAsync(brand);
                    }
                    await shopContext.SaveChangesAsync();

                }

            }

            if (!shopContext.Types.Any())
            {

                var TypeData = File.ReadAllText("../SnapShop.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await shopContext.Set<ProductType>().AddAsync(Type);
                    }
                    await shopContext.SaveChangesAsync();

                }
            }


            if (!shopContext.Products.Any())
            {
                var ProdutData = File.ReadAllText("../SnapShop.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProdutData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await shopContext.Set<Product>().AddAsync(product); 
                    }
                    await shopContext.SaveChangesAsync();

                }

            }



        }
    }
}
