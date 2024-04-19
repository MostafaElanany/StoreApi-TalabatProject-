using AutoMapper;
using dataa.Entities;
using dataa.Entities.OrderEntities;
using Repos.Spectiction.OrderSpec;

using Repos.BasketRepository.Models;
using Repos.Interfaces;
using service.Services.BasketServices;
using service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using service.Services.PaymentService;
using Stripe;
using Order = dataa.Entities.OrderEntities.Order;

namespace service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitWork _unitWork;
        private readonly IBasketServices _basketServices;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitWork unitWork,IBasketServices basketServices,
            IMapper mapper
            ,IPaymentService paymentService)
        {
            _unitWork = unitWork;
            _basketServices = basketServices;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
        {
            var basket = await _basketServices.GetBasketAysnc(input.BasketId);
            if(basket == null)
            {
                throw new Exception("Basket Not Exist");
            }
            var orderitems = new List<OrderItemDto>();
            foreach (var basketiem in basket.BasketItems)
            {
                var productitem = await _unitWork.Rep<dataa.Entities.Product, int>().getByIdAsync(basketiem.ProductId);
                if (productitem == null)
                {
                    throw new Exception($"product with id: {basketiem.ProductId}  Not Exist");
                }
                var itemOrderd = new ProductItemOrdered
                {
                    ProductItemId=productitem.Id,
                    ProductName=productitem.Name,
                    PictureUrl=productitem.PicURL

                };

                var orderitem = new OrderItem
                {
                    Price = productitem.price,
                    Quantity = basketiem.Quantity,
                    ItemOrdered = itemOrderd
                };
                var mappedorderItem = _mapper.Map <OrderItemDto>(orderitem);

                orderitems.Add(mappedorderItem);

            }
            var deliverymethod=await _unitWork.Rep<DeliveryMethod,int>().getByIdAsync(input.DeliveryMethodId);
            if(deliverymethod == null)
                throw new Exception($"DeliveryMethod not Provided");

            var subtotal = orderitems.Sum(item => item.Quantity * item.Price);

            var specs = new OrderWithPaymentIntSpecfiction(basket.PaymentIntentId);

            var existingOrder = await _unitWork.Rep<Order, Guid>().getWithSpecsByIdAsync(specs);
            if (existingOrder != null)
            {
                _unitWork.Rep<Order, Guid>().delete(existingOrder);
                await _paymentService.CreateOrUptadePaymentIntentForExistingOrder(basket);
            }
            else
            {
                await _paymentService.CreateOrUptadePaymentIntentForNewOrder(basket.Id);

            }

            var mappedShippingAdsress = _mapper.Map<ShippingAddress>(input.ShippingAddrees);
            var mappedOrderedItems = _mapper.Map<List<OrderItem>>(orderitems);
            var order = new Order
            {
                DeliveryMethodId = deliverymethod.Id,
                ShippingAddress = mappedShippingAdsress,
                BuyerEmail = input.BuyerEmail,
                OrderItems = mappedOrderedItems,
                SubTotal = subtotal,
                BasketId = basket.Id,
                PaymentIntentId=basket.PaymentIntentId

            };

            await _unitWork.Rep<Order, Guid>().addasync(order);
            await _unitWork.completeAsync();
            var mappedorder=_mapper.Map<OrderResultDto>(order);
            return mappedorder;


        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
           return await _unitWork.Rep<DeliveryMethod,int>().getallasync();
        }

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrderUserAsync(string BuyerEmail)
        {
            var specs = new OrderWithItemsSpecfiction(BuyerEmail);
            var orders= await _unitWork.Rep<Order,Guid>().getallWithSpecsasync(specs);

            if (orders is { Count: <= 0 })
                throw new Exception("You Do Not Have Any Order Yet ");
            var mappedorders = _mapper.Map<List<OrderResultDto>>(orders);
            return mappedorders;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id, string BuyerEmail)
        {
            var specs = new OrderWithItemsSpecfiction(id,BuyerEmail);
            var order = await _unitWork.Rep<Order, Guid>().getWithSpecsByIdAsync(specs);

            if(order is null )
                throw new Exception($"There is no order with Id : {id} ");    
            var mappedorders = _mapper.Map<OrderResultDto>(order);
            return mappedorders;
        }
    }
}
