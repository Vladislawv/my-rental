using MyRental.Services.Areas.Notifications.Data;

namespace MyRental.Services.Areas.Notifications;

public interface INotificationService
{
    public Task<IList<string>> GetListAsync();
    public Task NotifyAsync(Letter letter);
    public Task NotifyOfSubscribeAsync(string email);
    public Task NotifyOfRegisterAsync(string email);
    public Task SubscribeToNotificationsAsync(string email);
    public Task UnsubscribeFromNotificationsAsync(string email);
}