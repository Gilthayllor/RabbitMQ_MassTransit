namespace RabbitMQ_MassTransit.Shared.Abstractions
{
    public class Notification
    {
        public DateTime NotificationDate { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
