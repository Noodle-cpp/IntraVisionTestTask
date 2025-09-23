using Api.ViewModels.Requests;
using Api.ViewModels.Responses;
using Application.Abstractions.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SodasController : ControllerBase
    {
        private readonly  ISodaService _sodaService;
        private readonly  IMapper _mapper;

        public SodasController(ISodaService sodaService, IMapper mapper)
        {
            _sodaService = sodaService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<SodaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSodasAsync([FromQuery] SodaFilterRequest filterRequest, int page = 1, int perPage = 20)
        {
            try
            {
                var sodas = await _sodaService.GetSodasAsync(filterRequest.MinPrice, filterRequest.MaxPrice, page, perPage, filterRequest.BrandId).ConfigureAwait(false);
                var totalCount = await _sodaService.GetTotalSodasCountAsync(filterRequest.MinPrice, filterRequest.MaxPrice).ConfigureAwait(false);
                var list = _mapper.Map<IEnumerable<SodaResponse>>(sodas);
                
                var response = new PaginationResponse<SodaResponse>()
                {
                    List = list,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling((double)totalCount / perPage),
                };
                
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{sodaId}/img")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSodasAsync(Guid sodaId)
        {
            try
            {
                var img = await _sodaService.GetSodaImgByIdAsync(sodaId).ConfigureAwait(false);
                var result = new FileStreamResult(img, $"application/png")
                {
                    FileDownloadName = $"soda.png"
                };
                
                return result;
            }
            catch (SodaNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("price/range")]
        [ProducesResponseType(typeof(PriceRangeResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMinPriceAsync([FromQuery] Guid? brandId)
        {
            try
            {
                var minPrice = await _sodaService.GetMinPrice(brandId).ConfigureAwait(false);
                var maxPrice = await _sodaService.GetMaxPrice(brandId).ConfigureAwait(false);
                var response = new PriceRangeResponse()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
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
