using System;
using MortgagePricingService.Messaging.Handlers;
using MortgagePricingService.Messaging.Subscriber;

namespace MortgagePricingService.BackgroundServices
{
    public class PricingSubscriberWorker : BackgroundService
    {
        private readonly PricingSubscriber _subscriber;

        public PricingSubscriberWorker(PricingSubscriber subscriber)
        {
            Console.WriteLine("PricingSubscriberWorker created.");
            _subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("PricingSubscriberWorker executing...");
            await _subscriber.StartProcessingAsync(NotificationHandler.HandleNotificationAsync);
        }
    }
}

