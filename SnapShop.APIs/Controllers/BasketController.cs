using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnapShop.APIs.Errors;
using SnapShop.Core.Models;
using SnapShop.Core.Repositories;

namespace SnapShop.APIs.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
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
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedOrCreatedBasket = await _basketRepository.UpdateBasketAsync(basket);
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
