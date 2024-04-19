using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using service.Services.BasketServices.DTO;
using service.Services.OrderService.Dtos;
using service.Services.PaymentService;
using Stripe;

namespace StoreApi.Controllers
{
 
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private const string endpointSecret = "whsec_5d46090b9e8a69638e01aef97c3183b4ce584a0c13a730575b10e425ab916146";

        public PaymentController(IPaymentService paymentService
            ,ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUptadePaymentIntentForExistingOrder(CustomerBasketDto input)
            => Ok(  await _paymentService.CreateOrUptadePaymentIntentForExistingOrder(input));
       
        [HttpPost("{basketId}")]

        public async Task<ActionResult<CustomerBasketDto>> CreateOrUptadePaymentIntentForNewOrder(string basketId)
          => Ok(await _paymentService.CreateOrUptadePaymentIntentForNewOrder(basketId));



        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                PaymentIntent paymentIntent;
                OrderResultDto order;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed:", paymentIntent.Id);
                    order = await _paymentService.UptadePaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("order Updated to Payment Failed:", order.Id);

                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {

                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeed:", paymentIntent.Id);
                    order = await _paymentService.UptadePaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("order Updated to Payment Succeeded:", order.Id);
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
        

    }
}
