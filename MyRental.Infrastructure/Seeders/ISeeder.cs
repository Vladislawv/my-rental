using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;

namespace MyRental.Infrastructure.Seeders;

public interface ISeeder<TEntity>
    where TEntity : IEntity
{
    void Seed(ModelBuilder builder);
}