using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Notifications.Data;
using MyRental.Services.Exceptions;

namespace MyRental.Services.Areas.Notifications;

public class NotificationService : INotificationService
{
    private readonly IConfiguration _configuration;
    private readonly MyRentalContext _context;
    private readonly MailAddress _from;

    public NotificationService(IConfiguration configuration, MyRentalContext context)
    {
        _configuration = configuration;
        _context = context;

        _from = new MailAddress(_configuration["NetworkCredential:UserName"], "MyRental");
    }

    public async Task<IList<string>> GetListAsync()
    {
        var list = await _context.Mailing
            .Select(mail => mail.Email)
            .ToListAsync();

        return list;
    }

    public async Task NotifyAsync(Letter letter)
    {
        var client = GetClient();
        
        var message = new MailMessage
        {
            From = _from,
            Subject = letter.Title,
            Body = letter.Message
        };
        
        foreach (var email in await GetListAsync())
        {
            message.Bcc.Add(new MailAddress(email));
        }
        
        await client.SendMailAsync(message);
    }

    public async Task NotifyOfSubscribeAsync(string email)
    {
        var client = GetClient();

        var message = new MailMessage
        {
            From = _from,
            Subject = "You successfully subscribed!",
            Body = "Thank you for subscribe to our newsletter."
        };
        
        message.Bcc.Add(new MailAddress(email));

        await client.SendMailAsync(message);
    }
    
    public async Task NotifyOfRegisterAsync(string email)
    {
        var client = GetClient();

        var message = new MailMessage
        {
            From = _from,
            Subject = "You successfully Registered!",
            Body = "Thank you for registration."
        };

        message.Bcc.Add(new MailAddress(email));

        await client.SendMailAsync(message);
    }

    public async Task SubscribeToNotificationsAsync(string email)
    {
        var existingMail = await GetMailByEmail(email);

        if (existingMail != null) throw new BadRequestException("This email is already subscribed!");
        
        var inputMail = new Mail { Email = email };
        
        await _context.Mailing
            .AddAsync(inputMail);

        await _context.SaveChangesAsync();
    }

    public async Task UnsubscribeFromNotificationsAsync(string email)
    {
        var mail = await GetMailByEmail(email)
            ?? throw new BadRequestException("This email is not subscribed.");

        _context.Mailing
            .Remove(mail);

        await _context.SaveChangesAsync();
    }

    private async Task<Mail?> GetMailByEmail(string email)
    {
        return await _context.Mailing
            .FirstOrDefaultAsync(mail => mail.Email == email);
    }

    private SmtpClient GetClient()
    {
        return new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential
            {
                UserName = _configuration["NetworkCredential:UserName"],
                Password = _configuration["NetworkCredential:Password"]
            }
        };
    }
}