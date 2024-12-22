using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Services.MortgagePricing.Models;

namespace MortgagePricingService.Messaging.Handlers
{
    public class NotificationHandler
    {
        public static async Task HandleNotificationAsync(ProcessMessageEventArgs args)
        {
            var body = args.Message.Body.ToString();
            var update = JsonSerializer.Deserialize<PricingUpdate>(body);

            if (update?.LoanAmount <= 0) // Invalid data
            {
                // Explicitly dead-letter the message
                await args.DeadLetterMessageAsync(args.Message, "Invalid Loan Amount", "Loan amount cannot be zero or negative");
                return;
            }

            Console.WriteLine($"Notification sent for ApplicationId: {update?.LoanAmount}");

            //await args.CompleteMessageAsync(args.Message);
        }
    }
}

