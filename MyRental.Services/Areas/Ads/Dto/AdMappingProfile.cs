using AutoMapper;
using MyRental.Infrastructure.Entities;

namespace MyRental.Services.Areas.Ads.Dto;

public class AdMappingProfile : Profile
{
    public AdMappingProfile()
    {
        CreateMap<AdDtoInput, Advertisement>();
        CreateMap<Advertisement, AdDto>();
        CreateMap<AdDto, AdDtoInput>();
    }
}