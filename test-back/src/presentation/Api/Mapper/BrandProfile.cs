using Api.ViewModels.Responses;
using AutoMapper;
using Domain.Models;

namespace Api.Mapper;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandResponse>();
    }
}