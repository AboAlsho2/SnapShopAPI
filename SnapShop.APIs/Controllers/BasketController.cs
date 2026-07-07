using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnapShop.APIs.DTOs;
using SnapShop.APIs.Errors;
using SnapShop.Core.Models;
using SnapShop.Core.Repositories;

namespace SnapShop.APIs.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        //get or Recreate Basket
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
 
            return (basket is null) ?  new CustomerBasket(id) : Ok(basket);
        }

        //Update or Create Basket 
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
            var updatedOrCreatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (updatedOrCreatedBasket is null)
            {
                return BadRequest( new ApiResponse(400));
            }
            else
            {  
                return Ok(updatedOrCreatedBasket);
            }
        }


        //Delet
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletBasket(string id )
        {

            return await _basketRepository.DeleteBasketAsync(id);

        }
}}
