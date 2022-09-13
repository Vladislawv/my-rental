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
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user == null) throw new Exception($"User with Id:{id} is not found.");

        return _mapper.Map<UserDto>(user);
    }

    public async Task CreateAsync(UserDtoInput userDtoInput)
    {
        var user = _mapper.Map<User>(userDtoInput);

        if (user == null) throw new Exception("User must not be null.");

        await _userManager.CreateAsync(user);
    }

    public async Task<UserDto> UpdateAsync(int id, UserDtoInput userDtoInput)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id);
        
        if (user == null) throw new Exception($"User with Id:{id} is not found.");

        _mapper.Map(userDtoInput, user);
        
        var result =  await _userManager.UpdateAsync(user);

        if (!result.Succeeded) throw new Exception("An error occured while updating user");
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user == null) throw new Exception($"User with Id:{id} is not found.");

        await _userManager.DeleteAsync(user);
    }
}