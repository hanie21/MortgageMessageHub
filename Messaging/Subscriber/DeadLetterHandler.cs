using Azure.Messaging.ServiceBus;

namespace Services.MortgagePricing.Messaging.Subscriber
{
    public class DeadLetterHandler
    {
        private readonly ServiceBusReceiver _dlqReceiver;

        public DeadLetterHandler(ServiceBusClient client, string topicName, string subscriptionName)
        {
            var deadLetterPath = $"{topicName}/Subscriptions/{subscriptionName}/$DeadLetterQueue";
            _dlqReceiver = client.CreateReceiver(deadLetterPath);
        }

        public async Task ProcessDeadLetterMessagesAsync()
        {
            var messages = await _dlqReceiver.ReceiveMessagesAsync(maxMessages: 10);

            foreach (var message in messages)
            {
                Console.WriteLine($"Dead Letter: {message.Body}");
                // Log or process the dead-lettered message here

                // Complete the message
                await _dlqReceiver.CompleteMessageAsync(message);
            }
        }
    }
}