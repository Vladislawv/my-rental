using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Users.Dto;
using MyRental.Services.Handlers;

namespace MyRental.Services.Areas.Users.Services.UserService;

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
    
    public async Task<UserDto> GetByLoginAsync(LoginDto login)
    {
        if (string.IsNullOrEmpty(login.Login) || string.IsNullOrEmpty(login.Password)) 
            throw new Exception("Input data is empty.");

        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == login.Login || u.Email == login.Login)
                ?? throw new Exception("This user is not found.");
        
        var isPasswordMatch = await _userManager.CheckPasswordAsync(user, login.Password);

        return isPasswordMatch ? _mapper.Map<UserDto>(user) : throw new Exception("Password is not match");
    }
    
    public async Task<int> CreateAsync(UserDtoInput userInput)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(userInput.Email);

        if (userWithSameEmail != null) throw new Exception("This email is already used.");

        var userWithSamePhoneNumber = await _userManager.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == userInput.PhoneNumber);

        if (userWithSamePhoneNumber != null) throw new Exception("This Phone number is already taken.");

        var user = _mapper.Map<User>(userInput);
        
        var result = await _userManager.CreateAsync(user, userInput.Password);
        if (!result.Succeeded) throw new Exception(ErrorHandler.GetDescriptionByIdentityResult(result));
        
        await _userManager.AddToRoleAsync(user, userInput.Role);
        
        return user.Id;
    }
    
    public async Task<int> UpdateByIdAsync(int id, UserDtoInput userInput)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id) 
                ?? throw new Exception($"User with Id:{id} is not found.");

        if (user.Email != userInput.Email)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(userInput.Email);

            if (userWithSameEmail != null) throw new Exception("This email is already used.");
        }

        if (user.PhoneNumber != userInput.PhoneNumber)
        {
            var userWithSamePhoneNumber = await _userManager.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == userInput.PhoneNumber);

            if (userWithSamePhoneNumber != null) throw new Exception("This Phone number is already taken.");
        }

        _mapper.Map(userInput, user);
        
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new Exception(ErrorHandler.GetDescriptionByIdentityResult(result));

        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, userInput.Password);
        
        await _userManager.RemoveFromRoleAsync(user, user.Roles.ElementAt(0).Name);
        await _userManager.AddToRoleAsync(user, userInput.Role);
        
        return user.Id;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id)
                ?? throw new Exception($"User with Id:{id} is not found.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) throw new Exception(ErrorHandler.GetDescriptionByIdentityResult(result));
    }

    public async Task<string> GetRoleNameByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new Exception($"User with Id:{id} is not found.");

        return user.Roles.ElementAt(0).Name;
    }
    
    public async Task<(bool Result, string ErrorMessage)> ValidatePasswordAsync(string password)
    {
        var result = await new PasswordValidator<User>().ValidateAsync(_userManager, null, password);

        return (result.Succeeded, result.Errors.Aggregate("", (current, error) => (current + error.Description + " ")));
    }
}