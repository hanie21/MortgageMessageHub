using System;
using Azure.Messaging.ServiceBus;

namespace MortgagePricingService.Extensions
{
    public static class ServiceBusExtensions
    {
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services, string connectionString, string topicName, string subscriptionName)
        {
            var client = new ServiceBusClient(connectionString);
            services.AddSingleton(new Messaging.Publisher.PricingPublisher(client, topicName));
            services.AddSingleton(new Messaging.Subscriber.PricingSubscriber(client, topicName, subscriptionName));
            return services;
        }
    }
}

