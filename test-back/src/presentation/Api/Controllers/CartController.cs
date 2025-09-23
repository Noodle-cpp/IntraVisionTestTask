using Api.ViewModels.Requests;
using Api.ViewModels.Responses;
using Application.Abstractions.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CartListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartAsync()
        {
            try
            {
                var cart = await _cartService.GetCartsAsync().ConfigureAwait(false);
                var response = new CartListResponse()
                {
                    Carts = _mapper.Map<IEnumerable<CartResponse>>(cart.Carts),
                    Count = cart.Count,
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
