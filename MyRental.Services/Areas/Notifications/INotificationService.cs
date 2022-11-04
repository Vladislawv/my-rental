namespace MyRental.Services.Areas.Notifications;

public interface INotificationService
{
    public Task SubscribeToNotificationsAsync(string email);
    public Task UnsubscribeFromNotificationsAsync(string email);
}