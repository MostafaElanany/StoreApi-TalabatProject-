using dataa.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Spectiction
{
    public class ProductWithSpectictionsCountFilter : BaseSpectiction<Product>
    {
        public ProductWithSpectictionsCountFilter(ProductSpectiction specs) : base
            (
             product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                     (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value) &&
               (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))
            )
        {

        }
    }
}
