using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;

namespace MyRental.Services.Areas.Notifications;

public class NotificationService : INotificationService
{
    private readonly MyRentalContext _context;

    public NotificationService(MyRentalContext context)
    {
        _context = context;
    }

    public async Task SubscribeToNotificationsAsync(string email)
    {
        var existingMail = await GetMailByEmail(email);

        if (existingMail != null) throw new Exception("This email is already exist.");
        
        var inputMail = new Mail { Email = email };
        
        await _context.Mailing
            .AddAsync(inputMail);

        await _context.SaveChangesAsync();
    }

    public async Task UnsubscribeFromNotificationsAsync(string email)
    {
        var mail = await GetMailByEmail(email)
            ?? throw new Exception("Email is not found.");

        _context.Mailing
            .Remove(mail);

        await _context.SaveChangesAsync();
    }

    private async Task<Mail?> GetMailByEmail(string email)
    {
        return await _context.Mailing
            .FirstOrDefaultAsync(mail => mail.Email == email);
    }
}