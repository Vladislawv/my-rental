using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Task<List<UserDto>> GetListAsync()
    {
        var users = _userManager.Users.ToList();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
        var mapper = config.CreateMapper();

        var userDtoList = users.Select(user => mapper.Map<UserDto>(user)).ToList();

        return Task.FromResult(userDtoList);
    }

    public async Task<UserDto> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
        var mapper = config.CreateMapper();

        var userDto = mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task<UserDto> CreateAsync(User user)
    {
        await _userManager.CreateAsync(user);

        var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
        var mapper = config.CreateMapper();

        var userDto = mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task<UserDto> UpdateAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        await _userManager.UpdateAsync(user);

        var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
        var mapper = config.CreateMapper();

        var userDto = mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        await _userManager.DeleteAsync(user);
    }
}