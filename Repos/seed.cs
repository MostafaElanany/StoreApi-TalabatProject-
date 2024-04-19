using dataa.context;
using dataa.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repos
{
    public class seed
    {
      public static async Task seedasync(storecontext context,ILoggerFactory loggerFactory )
        {
            try
            {
                if(context.ProductBrands!=null && !context.ProductBrands.Any())
                {
                    var branddata = File.ReadAllText("../Repos/SeedData/brands.json");
                var brands=JsonSerializer.Deserialize<List<ProductBrand>>(branddata);

                    if(brands!=null ) 
                    {
                       
                            await context.ProductBrands.AddRangeAsync(brands);


                        
                    }
                }
                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesdata = File.ReadAllText("../Repos/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);

                    if (types != null)
                    {

                        await context.ProductTypes.AddRangeAsync(types);



                    }
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var productsdata = File.ReadAllText("../Repos/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsdata);

                    if (products != null)
                    {

                        await context.Products.AddRangeAsync(products);



                    }
                }
                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsdata = File.ReadAllText("../Repos/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsdata);

                    if (deliveryMethods != null)
                    {

                        await context.DeliveryMethods.AddRangeAsync(deliveryMethods);



                    }
                }
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<seed>();
                logger.LogError(ex.Message);
             
            }
        }

    }   
}
