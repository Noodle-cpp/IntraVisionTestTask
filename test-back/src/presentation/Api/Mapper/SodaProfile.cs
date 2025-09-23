using Api.ViewModels.Requests;
using Api.ViewModels.Responses;
using AutoMapper;
using Domain.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Api.Mapper;

public class SodaProfile : Profile
{
    public SodaProfile()
    {
        CreateMap<Soda, SodaResponse>();
    }
}