using Microsoft.EntityFrameworkCore;

namespace MyRental.Infrastructure;

public class MyRentalContext : DbContext
{
    public MyRentalContext(DbContextOptions<MyRentalContext> options) : base(options)
    {
    }
}