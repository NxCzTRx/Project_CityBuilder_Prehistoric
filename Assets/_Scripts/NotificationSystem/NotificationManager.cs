using _Scripts.Events;

namespace _Scripts.NotificationSystem
{
    public class NotificationManager
    {
        public void Notify(string message, float displayDuration) =>
            EventBus<OnGameNotification>.Publish(new OnGameNotification(message, displayDuration));
    }
}