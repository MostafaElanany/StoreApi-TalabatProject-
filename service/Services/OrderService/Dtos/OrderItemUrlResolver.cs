using AutoMapper;
using dataa.Entities;
using dataa.Entities.OrderEntities;
using Microsoft.Extensions.Configuration;
using service.Services.ProductServices.Dtos;

namespace service.Services.OrderService.Dtos
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;
        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.ItemOrdered.PictureUrl}";


            }
            return null;


        }
    }
}

    


    
