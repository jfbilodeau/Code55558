using System.Security;
using StackExchange.Redis;

Console.WriteLine("Starting Redis Demo...");

var connectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");

await using var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);

var database = redis.GetDatabase();

Console.WriteLine("Measuring latency...");
var latency = await database.PingAsync(); ;
Console.WriteLine($"Latency: {latency.TotalMilliseconds}ms");

var keyName = "App1:Component1:DbResult";

Console.WriteLine("Setting value...");
await database.StringSetAsync(keyName, "Hello World!", TimeSpan.FromSeconds(5));

Console.WriteLine("Getting value...");
var value = await database.StringGetAsync(keyName);

if (value.HasValue)
{
    Console.WriteLine($"Value: {value}");
}
else
{
    Console.WriteLine("Value not found!");
}

Thread.Sleep(6000);

Console.WriteLine("Getting value...");

value = await database.StringGetAsync(keyName);

if (value.HasValue)
{
    Console.WriteLine($"Value: {value}");
}
else
{
    Console.WriteLine("Value not found!");
}

Console.WriteLine("Done!");