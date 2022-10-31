using AutoMapper;
using MyRental.Infrastructure.Entities;

namespace MyRental.Services.Areas.Medias.Dto;

public class MediaMappingProfile : Profile
{
    public MediaMappingProfile()
    {
        CreateMap<MediaDtoInput, MediaFile>();
        CreateMap<MediaFile, MediaDto>();
        CreateMap<MediaDto, MediaDtoInput>();
    }
}