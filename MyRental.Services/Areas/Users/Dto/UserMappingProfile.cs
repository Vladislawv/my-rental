using AutoMapper;
using MyRental.Infrastructure.Entities;

namespace MyRental.Services.Areas.Users.Dto;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserDtoInput, User>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, UserDtoInput>();
    }
}