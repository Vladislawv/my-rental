using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Users.Dto;
using MyRental.Services.Exceptions;
using MyRental.Services.Handlers;

namespace MyRental.Services.Areas.Users;

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
                ?? throw new NotFoundException($"User with Id:{id} is not found.");
        
        return user;
    }
    
    public async Task<UserDto> GetByLoginAsync(LoginDto login)
    {
        var user = await _userManager.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Email == login.Email)
                ?? throw new NotFoundException("This user is not found.");

        return user;
    }
    
    public async Task<int> CreateAsync(UserDtoInput userInput)
    {
        await CheckIfEmailIsFreeAsync(userInput.Email);

        await CheckIfPhoneNumberIsFreeAsync(userInput.PhoneNumber);

        var user = _mapper.Map<User>(userInput);
        
        var result = await _userManager.CreateAsync(user, userInput.Password);
        if (!result.Succeeded) throw new BadRequestException(ErrorHandler.GetDescriptionByIdentityResult(result));
        
        await _userManager.AddToRoleAsync(user, "User");
        
        return user.Id;
    }
    
    public async Task<int> UpdateByIdAsync(int id, UserDtoInput userInput)
    {
        var user = await _userManager.Users
            .Include(user => user.Advertisements)
            .FirstOrDefaultAsync(user => user.Id == id)
                ?? throw new NotFoundException($"User with Id:{id} is not found.");

        if (user.Email != userInput.Email) await CheckIfEmailIsFreeAsync(userInput.Email);

        if (user.PhoneNumber != userInput.PhoneNumber) await CheckIfPhoneNumberIsFreeAsync(userInput.PhoneNumber);

        _mapper.Map(userInput, user);
        
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new BadRequestException(ErrorHandler.GetDescriptionByIdentityResult(result));

        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, userInput.Password);
        
        return user.Id;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(user => user.Id == id)
                ?? throw new NotFoundException($"User with Id:{id} is not found.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) throw new BadRequestException(ErrorHandler.GetDescriptionByIdentityResult(result));
    }

    public async Task<(bool Result, string ErrorMessage)> ValidatePasswordAsync(string password)
    {
        var result = await new PasswordValidator<User>().ValidateAsync(_userManager, null, password);

        return (result.Succeeded, result.Errors.Aggregate("", (current, error) => (current + error.Description + " ")));
    }

    private async Task CheckIfEmailIsFreeAsync(string email)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(email);

        if (userWithSameEmail != null) throw new BadRequestException("This email is already used.");
    }

    private async Task CheckIfPhoneNumberIsFreeAsync(string phoneNumber)
    {
        var userWithSamePhoneNumber = await _userManager.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        if (userWithSamePhoneNumber != null) throw new BadRequestException("This Phone number is already taken.");
    }
}