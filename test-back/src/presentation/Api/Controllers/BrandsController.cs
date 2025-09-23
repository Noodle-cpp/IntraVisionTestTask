using Api.ViewModels.Responses;
using Application.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService  _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BrandResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrandsAsync()
        {
            try
            {
                var brands = await _brandService.GetBrandsAsync().ConfigureAwait(false);
                var response = brands.Select(b => new BrandResponse()
                {
                    Id = b.Id,
                    Name = b.Name,
                });
                
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
