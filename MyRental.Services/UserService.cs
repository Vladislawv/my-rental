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
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new Exception($"User with Id:{id} is not found.");
        
        return user;
    }

    public async Task<int> CreateAsync(UserDtoInput userInput)
    {
        var user = _mapper.Map<User>(userInput);
        
        await _userManager.CreateAsync(user, userInput.Password);
        
        return user.Id;
    }
    
    public async Task<int> UpdateAsync(int id, UserDtoInput userInput)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id) 
                ?? throw new Exception($"User with Id:{id} is not found.");
        
        _mapper.Map(userInput, user);
       
        await _userManager.UpdateAsync(user);

        return user.Id;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id)
                ?? throw new Exception($"User with Id:{id} is not found.");

        await _userManager.DeleteAsync(user);
    }

    public async Task<(bool Result, string ErrorMessage)> ValidatePasswordAsync(string password)
    {
        var passwordValidator = new PasswordValidator<User>();
        
        var result = await passwordValidator.ValidateAsync(_userManager, null, password);

        return (result.Succeeded, result.Errors.Aggregate("", (current, error) => (current + error.Description + " ")));
    }
}