using AutoMapper;
using dataa.Entities.IdentityEntities;
using dataa.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.OrderService.Dtos
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<AddressDto, ShippingAddress>().ReverseMap();
            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.DeliveryMethodName, op => op.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, op => op.MapFrom(src => src.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, op => op.MapFrom(src => src.ItemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductName, op => op.MapFrom(src => src.ItemOrdered.ProductName))
                .ForMember(dest => dest.PictureUrl, op => op.MapFrom(src => src.ItemOrdered.PictureUrl))
                 .ForMember(dest => dest.PictureUrl, op => op.MapFrom<OrderItemUrlResolver>()).ReverseMap();




        }

    }
}
