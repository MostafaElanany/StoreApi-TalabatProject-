using AutoMapper;
using AutoMapper.Execution;
using dataa.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.ProductServices.Dtos
{
    public class productResolver : IValueResolver<Product, ProductDetailsDto, string>
    {
        private readonly IConfiguration _configuration;

        public productResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PicURL))
            {
                return $"{ _configuration["BaseUrl"]}{source.PicURL}";


            }
            return null;
         

        }
    }
}
