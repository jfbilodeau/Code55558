using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus_Demo
{
    class Receiver
    {
        const string ServiceBusConnectionString = "<put connectionstring here, remove queue if at the end>";
        const string QueueName = "<queue name>";
        static IQueueClient queueClient;

        public static async Task Main(string[] args)
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            Console.WriteLine("======================================================");
            Console.WriteLine("Press Ctrl-C to exit after receiving all the messages.");
            Console.WriteLine("======================================================");
            // Receive messages.
            await ReceiveMessagesAsync();
            Console.ReadKey();
            await queueClient.CloseAsync();
        }

        static async Task ReceiveMessagesAsync()
        {
            try
            {   
                queueClient.RegisterMessageHandler(
                    async (message, token) =>
                    {
                        var messageBody = Encoding.UTF8.GetString(message.Body);

                        Console.WriteLine($"Received message with: {messageBody}");

                        await queueClient.CompleteAsync(message.SystemProperties.LockToken);
                    },
                    new MessageHandlerOptions(async args => Console.WriteLine(args.Exception))
                    { MaxConcurrentCalls = 1, AutoComplete = false });
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
