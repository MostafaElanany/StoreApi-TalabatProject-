using Repos.Spectiction;
using service.Helper;
using service.Services.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.ProductServices
{
    public interface IProductService
    {


        Task<ProductDetailsDto> GeProductByAsync(int? id);

        Task<PaginatedResultDto<ProductDetailsDto>> GetAllproductsAsync(ProductSpectiction input);

        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypeSAsync();


    }
}
