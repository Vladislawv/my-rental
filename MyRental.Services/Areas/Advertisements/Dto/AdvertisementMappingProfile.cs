using AutoMapper;
using MyRental.Infrastructure.Entities;

namespace MyRental.Services.Areas.Advertisements.Dto;

public class AdvertisementMappingProfile : Profile
{
    public AdvertisementMappingProfile()
    {
        CreateMap<AdvertisementDtoInput, Advertisement>();
        CreateMap<Advertisement, AdvertisementDto>();
        CreateMap<AdvertisementDto, AdvertisementDtoInput>();
    }
}