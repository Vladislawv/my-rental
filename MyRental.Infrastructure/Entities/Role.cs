using Microsoft.AspNetCore.Identity;
using MyRental.Infrastructure.Enums;

namespace MyRental.Infrastructure.Entities;

public class Role : IdentityRole<int>, IEntity
{
    public RoleType Type { get; set; }

    public virtual ICollection<User> Users { get; set; }
}