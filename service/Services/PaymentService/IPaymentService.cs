using Repos.BasketRepository.Models;
using service.Services.BasketServices.DTO;
using service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUptadePaymentIntentForExistingOrder(CustomerBasketDto input);
        Task<CustomerBasketDto> CreateOrUptadePaymentIntentForNewOrder(string basketid);
        Task<OrderResultDto> UptadePaymentSucceeded(string paymentintentid);
        Task<OrderResultDto> UptadePaymentFailed(string paymentintentid);

    }
}
