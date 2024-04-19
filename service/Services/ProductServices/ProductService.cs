using AutoMapper;
using dataa.Entities;
using Repos.Interfaces;
using Repos.Spectiction;
using service.Helper;
using service.Services.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitWork unitWork,IMapper mapper)
        {
            _unitWork = unitWork;
              _mapper = mapper;
        }

        public async  Task<ProductDetailsDto> GeProductByAsync(int? id)
        {
            if (id == null)
                throw new Exception("id is null");
            var specs = new ProductWithSpectictions(id);

            var product = await _unitWork.Rep<Product, int>().getWithSpecsByIdAsync(specs);

            if (product == null)
                throw new Exception("product not found");

            var mapperproduct = _mapper.Map<ProductDetailsDto>(product);
            return mapperproduct;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitWork.Rep<ProductBrand, int>().getallasync();
            var mapperbrands=_mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mapperbrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllproductsAsync(ProductSpectiction input )
        {

            var specs = new ProductWithSpectictions(input);
            var products = await _unitWork.Rep<Product, int>().getallWithSpecsasync(specs);
            var countspecs = new ProductWithSpectictionsCountFilter(input);
            var count = await _unitWork.Rep<Product, int>().CountSpectionssasync(countspecs);
            var mapperproducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            return new PaginatedResultDto<ProductDetailsDto>(input.PageIndex,input.PageSize, count, mapperproducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypeSAsync()
        {
            var types = await _unitWork.Rep<ProductType, int>().getallasync();
            var mappertypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappertypes;
        }
    }
}
