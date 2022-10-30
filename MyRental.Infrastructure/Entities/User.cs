using Microsoft.AspNetCore.Identity;

namespace MyRental.Infrastructure.Entities;

public class User : IdentityUser<int>, IEntity
{
   public virtual ICollection<Role> Roles { get; set; }
   public virtual ICollection<Advertisement> Ads { get; set; }
}