using dataa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Spectiction
{
    public class ProductWithSpectictions:BaseSpectiction<Product>
    {

        public ProductWithSpectictions(ProductSpectiction specs) : base
            (
            product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                     (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value) &&
                     (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))
            )
        {
            AddInclude(x => x.Brand);
            AddInclude(x=> x.Type);
            AddOrderBy(x => x.Name);

            ApplyPagination(specs.PageSize * (specs.PageIndex - 1), specs.PageSize);

            if(!string.IsNullOrEmpty(specs.Sort))
            {

                switch(specs.Sort)
                {
                    case "priceAsc":
                      AddOrderBy(x => x.price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;

                }
            }
            
        }
        public ProductWithSpectictions(int? id) : base( product => product.Id==id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);

        }

    }
}
