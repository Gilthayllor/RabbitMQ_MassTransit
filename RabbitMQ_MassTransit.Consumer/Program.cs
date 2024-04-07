using MassTransit;
using RabbitMQ_MassTransit.Consumer.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<NotificationConsumer>();

    busConfigurator.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("RabbitMQSettings:Host" ?? ""), h =>
        {
            h.Username(builder.Configuration.GetValue<string>("RabbitMQSettings:Username" ?? ""));
            h.Password(builder.Configuration.GetValue<string>("RabbitMQSettings:Password" ?? ""));
        });

        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
