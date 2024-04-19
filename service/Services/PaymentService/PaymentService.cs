using dataa.Entities;
using dataa.Migrations;
using Microsoft.Extensions.Configuration;
using Repos.BasketRepository.Models;
using Repos.Interfaces;
using Repos.Spectiction.OrderSpec;
using service.Services.BasketServices;
using service.Services.BasketServices.DTO;
using service.Services.OrderService.Dtos;
using Stripe;
using dataa.Entities.OrderEntities;
using Order = dataa.Entities.OrderEntities.Order;
using AutoMapper;

namespace service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketServices _basketServices;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration,IBasketServices basketServices,
            IUnitWork unitWork,IMapper mapper)
        {
               _configuration = configuration;
            _basketServices = basketServices;
            _unitWork = unitWork;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUptadePaymentIntentForExistingOrder(CustomerBasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            if (basket == null)
                throw new Exception("Basket is null "); 

            var deliveryMethod = await _unitWork.Rep<DeliveryMethod, int>().getByIdAsync(basket.DeliveryMethodId.Value);
            var shippingPrice = deliveryMethod.Price;

            foreach (var item in basket.BasketItems)
            {
                var product = await _unitWork.Rep<dataa.Entities.Product, int>().getByIdAsync(item.ProductId);
                if (item.Price != product.price)
                    item.Price = product.price;
            }
            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {

                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),

                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {

                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),

                  
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketServices.updateBasketAsync(basket);
            return basket;

        }


        public  async Task<CustomerBasketDto> CreateOrUptadePaymentIntentForNewOrder(string basketid)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            var basket = await _basketServices.GetBasketAysnc(basketid);
            if (basket == null)
                throw new Exception("Basket is null ");

            var deliveryMethod = await _unitWork.Rep<DeliveryMethod, int>().getByIdAsync(basket.DeliveryMethodId.Value);
            var shippingPrice = deliveryMethod.Price;

            foreach (var item in basket.BasketItems)
            {
                var product = await _unitWork.Rep<dataa.Entities.Product, int>().getByIdAsync(item.ProductId);
                if (item.Price != product.price)
                    item.Price = product.price;
            }
            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {

                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),

                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {

                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),


                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketServices.updateBasketAsync(basket);
            return basket;
        }

        public async Task<OrderResultDto> UptadePaymentFailed(string paymentintentid)
        {
            var specs = new OrderWithPaymentIntSpecfiction(paymentintentid);
            var order = await _unitWork.Rep<Order, Guid>().getWithSpecsByIdAsync(specs);
            if (order is null)
                throw new Exception("Order Does not Exist");

            order.orderPaymentStatus = Order.OrderPaymentStatus.Faild;

            _unitWork.Rep<Order, Guid>().update(order);
            await _unitWork.completeAsync();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }

        public async Task<OrderResultDto> UptadePaymentSucceeded(string paymentintentid)
        {
            var specs = new OrderWithPaymentIntSpecfiction(paymentintentid);
            var order = await _unitWork.Rep<Order, Guid>().getWithSpecsByIdAsync(specs);
            if (order is null)
                throw new Exception("Order Does not Exist");

            order.orderPaymentStatus = Order.OrderPaymentStatus.Received;

            _unitWork.Rep<Order, Guid>().update(order);
            await _unitWork.completeAsync();
            await _basketServices.DeleteBasketAsync(order.BasketId); 
            var mappedOrder = _mapper.Map<OrderResultDto>(order);
           
            return mappedOrder;
        }
    }
}
