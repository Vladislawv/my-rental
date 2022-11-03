namespace MyRental.Infrastructure.Entities;

public class Mail : IEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
}