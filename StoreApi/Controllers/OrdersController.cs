using dataa.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using service.HandlerResponse;
using service.Services.OrderService;
using service.Services.OrderService.Dtos;
using System.Security.Claims;

namespace StoreApi.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto input)
        {
            var order = await _orderService.CreateOrderAsync(input);
            if (order == null)
                return BadRequest(new Response(400, "Error While Creating Your Order"));

            return Ok(order);


        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrderForUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetAllOrderUserAsync(email);
            return Ok(orders);

        }
        [HttpGet]

        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdAsync(id,email);
            return Ok(order);

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodsAsync()
        => Ok(await _orderService.GetAllDeliveryMethodsAsync());



    }
} 
