using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Handlers;

namespace MyRental.Services.Areas.Users.Services.RoleService;

public class RoleService : IRoleService
{
    private readonly UserManager<User> _userManager;

    public RoleService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task AddAdminRoleByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == id)
                 ?? throw new Exception($"User with Id:{id} is not found.");

        await _userManager.RemoveFromRoleAsync(user, user.Roles.ElementAt(0).Name);
        
        var result = await _userManager.AddToRoleAsync(user, "Admin");
        if (!result.Succeeded) throw new Exception(ErrorHandler.GetDescriptionByIdentityResult(result));
    }

    public async Task RemoveAdminRoleByIdAsync(int id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == id) 
                ?? throw new Exception($"User with Id:{id} is not found.");
        
        var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
        if (!result.Succeeded) throw new Exception(ErrorHandler.GetDescriptionByIdentityResult(result));

        await _userManager.AddToRoleAsync(user, "Tenant");
    }
}