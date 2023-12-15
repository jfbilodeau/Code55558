using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus_Demo
{
    class Sender
    {
        const string ServiceBusConnectionString = "<put connectionstring here, remove queue if at the end>";
        const string QueueName = "<queue name>";
        static IQueueClient queueClient;

        public static async Task Main(string[] args)
        {
            const int numberOfMessages = 50;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");
            // Send messages.
            Console.ReadKey();
            await SendMessagesAsync(numberOfMessages);
            Console.ReadKey();
            await queueClient.CloseAsync();
        }
               
        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the queue.
                    string messageBody = $"Message {i} {System.DateTime.Now.ToShortTimeString()}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");
                    // Send the message to the queue.
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
      
    }
}
