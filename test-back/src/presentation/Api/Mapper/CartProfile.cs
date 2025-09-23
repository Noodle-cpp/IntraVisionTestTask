using Api.ViewModels.Responses;
using AutoMapper;
using Domain.Models;

namespace Api.Mapper;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartResponse>();
    }
}