using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Advertisements.Dto;

namespace MyRental.Services.Areas.Users.Dto;

public class UserDto : EntityDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public IList<Role> Roles { get; set; }

    public IList<AdvertisementDto> Advertisements { get; set; }
}