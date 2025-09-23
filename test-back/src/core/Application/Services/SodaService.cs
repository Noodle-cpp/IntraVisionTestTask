using Application.Abstractions.Interfaces;
using Application.Exceptions;
using Application.ViewModels;
using Domain.Abstractions.Interfaces;
using Domain.Models;

namespace Application.Services;

public class SodaService : ISodaService
{
    private readonly ISodaRepository _sodaRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public SodaService(ISodaRepository sodaRepository, IHttpClientFactory httpClientFactory)
    {
        _sodaRepository = sodaRepository;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Soda>> GetSodasAsync(int minPrice, int maxPrice, int page, int perPage, Guid? brandId)
    {
        return await _sodaRepository.GetSodasAsync(minPrice, maxPrice, page, perPage, brandId).ConfigureAwait(false);
    }

    public async Task<int> GetTotalSodasCountAsync(int minPrice, int maxPrice, Guid? brandId = null)
    {
        return await _sodaRepository.GetTotalSodasCountAsync(minPrice, maxPrice, brandId).ConfigureAwait(false);
    }

    public async Task<int> GetMinPrice(Guid? brandId = null)
    {
        return await _sodaRepository.GetMinPriceAsync(brandId).ConfigureAwait(false);
    }

    public async Task<int> GetMaxPrice(Guid? brandId = null)
    {
        return await _sodaRepository.GetMaxPriceAsync(brandId).ConfigureAwait(false);
    }

    public async Task<int> GetTotalCountAsync(int minPrice, int maxPrice, Guid? brandId = null)
    {
        return await _sodaRepository.GetTotalCountAsync(minPrice, maxPrice, brandId).ConfigureAwait(false);
    }

    public async Task<MemoryStream> GetSodaImgByIdAsync(Guid sodaId)
    {
        var soda = await _sodaRepository.GetSodaByIdAsync(sodaId).ConfigureAwait(false) ?? throw new SodaNotFoundException(nameof(sodaId));
        
        var httpClient = _httpClientFactory.CreateClient();
        
        var response = await httpClient.GetAsync(soda.ImgPath).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new DownloadFileException("Фото не найдено");

        var fileStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

        var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}