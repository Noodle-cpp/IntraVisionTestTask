using Api.ViewModels.Responses;
using Application.Abstractions.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinService  _coinService;
        private readonly IMapper _mapper;

        public CoinsController(ICoinService coinService, IMapper mapper)
        {
            _coinService = coinService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CoinResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoinsAsync()
        {
            try
            {
                var coins = await _coinService.GetCoinsAsync().ConfigureAwait(false);
                var response = _mapper.Map<IEnumerable<CoinResponse>>(coins);
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
