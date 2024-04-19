using AutoMapper;
using Repos.BasketRepository;
using Repos.BasketRepository.Models;
using service.Services.BasketServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.BasketServices
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketServices(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _basketRepository.DeleteBasketAsync(basketid);
        }

        public async Task<CustomerBasketDto> GetBasketAysnc(string basketid)
        {
           var basket = await _basketRepository.GetBasketAysnc(basketid);

            if(basket == null)
                return new CustomerBasketDto();

            var mappedBasket=_mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;
        }

        public async Task<CustomerBasketDto> updateBasketAsync(CustomerBasketDto basket)
        {
            var CustomeerBasket= _mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await _basketRepository.updateBasketAsync(CustomeerBasket);
            var mappedCustomeerBasket = _mapper.Map<CustomerBasketDto>(updatedBasket);
            return mappedCustomeerBasket;



        }
    }
}
