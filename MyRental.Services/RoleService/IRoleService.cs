namespace MyRental.Services.RoleService;

public interface IRoleService
{
    public Task<bool> AddAdminRoleByIdAsync(int id);
    public Task<bool> RemoveAdminRoleByIdAsync(int id);
}