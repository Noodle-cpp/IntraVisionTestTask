using System.Transactions;
using Api.ViewModels.Requests;
using Api.ViewModels.Responses;
using Application.Abstractions.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICoinService _coinService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper, ICoinService coinService)
        {
            _cartService = cartService;
            _mapper = mapper;
            _coinService = coinService;
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
                    TotalPrice = cart.TotalPrice
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("add/sodas/{sodaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddSodaAsync(Guid sodaId)
        {
            try
            {
                await _cartService.CreateCartAsync(sodaId);
                return Ok();
            }
            catch (SodaNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{cartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCartAsync(Guid cartId, [FromBody] UpdateCartRequest request)
        {
            try
            {
                var updatedCart = _mapper.Map<Cart>(request);
                await _cartService.UpdateCartAsync(cartId, updatedCart).ConfigureAwait(false);
                return Ok();
            }
            catch (CartNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{cartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCartAsync(Guid cartId)
        {
            try
            {
                await _cartService.DeleteCartByIdAsync(cartId).ConfigureAwait(false);
                return Ok();
            }
            catch (CartNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("buy")]
        [ProducesResponseType(typeof(IEnumerable<CoinResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuyCartAsync([FromBody] PaymentRequest request)
        {
            try
            {
                using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                
                var coins = _mapper.Map<IEnumerable<Coin>>(request.Coins);
                var change = await _cartService.BuyCartAsync(coins).ConfigureAwait(false);
                var changeCoin = await _coinService.ChangeOfCoinAsync(change).ConfigureAwait(false); 
                await _coinService.CountNewBalanceAsync(changeCoin, coins).ConfigureAwait(false);

                transaction.Complete();

                return Ok(_mapper.Map<IEnumerable<CoinResponse>>(changeCoin.OrderBy(c => c.Banknote)));
            }
            catch (InsufficientFundsException e)
            {
                return BadRequest(e.Message);
            }
            catch (InsufficientSodaException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (CoinNotFoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
