using MassTransit;
using RabbitMQ_MassTransit.Shared.Abstractions;
using System.Text.Json;

namespace RabbitMQ_MassTransit.Consumer.Consumers
{
    public class NotificationConsumer : IConsumer<Notification>
    {
        public Task Consume(ConsumeContext<Notification> context)
        {
            Console.WriteLine(context.Message.Message); 
            return Task.CompletedTask;
        }
    }
}
