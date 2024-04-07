using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ_MassTransit.Shared.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("RabbitMQSettings:Host" ?? ""), h =>
        {
            h.Username(builder.Configuration.GetValue<string>("RabbitMQSettings:Username" ?? ""));
            h.Password(builder.Configuration.GetValue<string>("RabbitMQSettings:Password" ?? ""));
        });
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("notifications/send-notification", ([FromBody] Notification notification, [FromServices] IPublishEndpoint publishEndpoint) =>
{
    publishEndpoint.Publish(notification);

    return Results.Ok("Notification sent");
});

app.Run();




