using System;
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Services.MortgagePricing.Models;

namespace MortgagePricingService.Messaging.Publisher
{
    public class PricingPublisher
    {
        private readonly ServiceBusSender _sender;

        public PricingPublisher(ServiceBusClient client, string topicName)
        {
            _sender = client.CreateSender(topicName);

        }

        public async Task PublishPricingUpdateAsync(PricingUpdate update, DateTimeOffset scheduleTime)
        {
            var messageBody = JsonSerializer.Serialize(update);
            var message = new ServiceBusMessage(messageBody)
            {
                ScheduledEnqueueTime = scheduleTime
            };
            await _sender.SendMessageAsync(message);
        }
    }
}

