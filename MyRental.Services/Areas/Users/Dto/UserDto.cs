namespace MyRental.Services.Areas.Users.Dto;

public class UserDto : EntityDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}