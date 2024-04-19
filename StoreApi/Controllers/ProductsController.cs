using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repos.Spectiction;
using service.Helper;
using service.Services.ProductServices;
using service.Services.ProductServices.Dtos;
using StoreApi.Helper;

namespace StoreApi.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [cache(90)]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
         => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
         => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<PaginatedResultDto<ProductDetailsDto>>> GetAllProducts([FromQuery]ProductSpectiction input)
        => Ok(await _productService.GetAllproductsAsync(input));
        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>> GetProduct(int? id)
      => Ok(await _productService.GeProductByAsync(id));
    }
   

}
