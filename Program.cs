using Azure.Messaging.ServiceBus;
using MortgagePricingService.BackgroundServices;
using MortgagePricingService.Extensions;
using MortgagePricingService.Services;
using Services.MortgagePricing.Messaging.Subscriber;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve sensitive data from environment variables
var serviceBusConnectionString = Environment.GetEnvironmentVariable("SERVICE_BUS_CONNECTION_STRING");
var topicName = Environment.GetEnvironmentVariable("TOPIC_NAME");
var subscriptionName = Environment.GetEnvironmentVariable("SUBSCRIPTION_NAME");

// Add services
builder.Services.AddSingleton<PricingCalculatorService>();

builder.Services.AddAzureServiceBus(serviceBusConnectionString, topicName, subscriptionName);

builder.Services.AddSingleton<DeadLetterHandler>(provider =>
{
    var client = new ServiceBusClient(serviceBusConnectionString);
    return new DeadLetterHandler(client, topicName, subscriptionName);
});

builder.Services.AddHostedService<PricingSubscriberWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();