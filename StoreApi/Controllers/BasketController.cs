using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using service.Services.BasketServices;
using service.Services.BasketServices.DTO;

namespace StoreApi.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketByid(string id)
            => Ok(await _basketServices.GetBasketAysnc(id));
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdatetBasketAsync(CustomerBasketDto basket)
        => Ok(await _basketServices.updateBasketAsync(basket));

        [HttpDelete]

        public async Task<ActionResult> DeleteBasketAsync(string id)
     => Ok(await _basketServices.DeleteBasketAsync(id));



    }
}
