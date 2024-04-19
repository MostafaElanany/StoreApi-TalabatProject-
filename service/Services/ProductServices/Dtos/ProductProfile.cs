using AutoMapper;
using dataa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.ProductServices.Dtos
{
    public class ProductProfile:Profile
    {

        public ProductProfile() 
        {
            CreateMap<dataa.Entities.Product, ProductDetailsDto>()
            .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.Type.Name))
           .ForMember(dest => dest.PicURL, options => options.MapFrom<productResolver>());

            CreateMap<ProductBrand, BrandTypeDetailsDto>();
            CreateMap<ProductType, BrandTypeDetailsDto>();


        }
    }
}
