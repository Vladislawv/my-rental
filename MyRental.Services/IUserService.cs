using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services;

public interface IUserService
{
    public Task<List<UserDto>> GetListAsync();
    public Task<UserDto> GetByIdAsync(string id);
    public Task<UserDto> CreateAsync(User user);
    public Task<UserDto> UpdateAsync(string id);
    public Task DeleteAsync(string id);
}