using Api.ViewModels.Requests;
using Api.ViewModels.Responses;
using AutoMapper;
using Domain.Models;

namespace Api.Mapper;

public class CoinProfile : Profile
{
    public CoinProfile()
    {
        CreateMap<CoinRequest, Coin>().ReverseMap();
        CreateMap<Coin, CoinResponse>().ReverseMap();
    }
}