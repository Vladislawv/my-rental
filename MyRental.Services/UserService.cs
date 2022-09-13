using System.Security.Cryptography;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IList<UserDto>> GetListAsync()
    {
        return await _userManager.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userManager.Users
            .Where(user => user.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (user == null) throw new Exception($"User with Id:{id} is not found.");

        return user;
    }

    public async Task<int> CreateAsync(UserDtoInput userDtoInput)
    {
        var user = _mapper.Map<User>(userDtoInput);

        await _userManager.CreateAsync(user);

        return user.Id;
    }
    
    public async Task UpdateAsync(int id, UserDtoInput userDtoInput)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id);
        
        if (user == null) throw new Exception($"User with Id:{id} is not found.");

        _mapper.Map(userDtoInput, user);
        
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user == null) throw new Exception($"User with Id:{id} is not found.");

        await _userManager.DeleteAsync(user);
    }
}