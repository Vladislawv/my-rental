namespace MyRental.Services.Areas.Users.Services.RoleService;

public interface IRoleService
{
    public Task AddAdminRoleByIdAsync(int id);
    public Task RemoveAdminRoleByIdAsync(int id);
}