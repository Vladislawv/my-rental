using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Users.Dto;
using MyRental.Services.Validators;

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
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == id)
                   ?? throw new Exception($"User with Id:{id} is not found.");

        return user;
    }

    public async Task<int> CreateAsync(UserDtoInput userInput)
    {
        var password = userInput.Password;

        if (!PasswordValidator.Validate(password)) throw new Exception("Password must contain: upperCase, lowerCase, digit, non alphanumeric symbol, minimum length: 6.");

        var user = _mapper.Map<User>(userInput);
        
        await _userManager.CreateAsync(user, password);

        return user.Id;
    }
    
    public async Task UpdateAsync(int id, UserDtoInput userInput)
    {
        var password = userInput.Password;

        if (!PasswordValidator.Validate(password)) throw new Exception("Password must contain: upperCase, lowerCase, digit, non alphanumeric symbol, minimum length: 6.");

        var user = await _userManager.Users
                       .FirstOrDefaultAsync(user => user.Id == id)
                   ?? throw new Exception($"User with Id:{id} is not found.");

        _mapper.Map(userInput, user);
        
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id)
                   ?? throw new Exception($"User with Id:{id} is not found.");
        
        await _userManager.DeleteAsync(user);
    }
}