using System;
using Azure.Messaging.ServiceBus;

namespace MortgagePricingService.Messaging.Subscriber
{
    public class PricingSubscriber
    {
        private readonly ServiceBusProcessor _processor;

        public PricingSubscriber(ServiceBusClient client, string topicName, string subscriptionName)
        {
            _processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false // Default behavior
            });
        }

        public async Task StartProcessingAsync(Func<ProcessMessageEventArgs, Task> messageHandler)
        {
            Console.WriteLine("Initializing Service Bus Processor...");
            _processor.ProcessMessageAsync += messageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
            Console.WriteLine("Service Bus Processor started.");
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error: {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}

