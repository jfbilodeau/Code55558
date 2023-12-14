using Microsoft.Azure.Cosmos;

Console.WriteLine("Starting Cosmos DB Demo...");

var uri = Environment.GetEnvironmentVariable("COSMOSDB_URI");
var key = Environment.GetEnvironmentVariable("COSMOSDB_KEY");

var client = new CosmosClient(
    uri, 
    key,
    new CosmosClientOptions()
    {
        SerializerOptions = new CosmosSerializationOptions()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    }
);

var database = await client.CreateDatabaseIfNotExistsAsync("hr");

var getContainerResult = await database.Database.CreateContainerIfNotExistsAsync("employees", "/id");
var container = getContainerResult.Container;

// var employee = new Employee()
// {
//     Id = "EMP1008",
//     FirstName = "Allan",
//     LastName = "Turing",
//     Dept = "FN",
//     Salary = 160000
// };

// var response = await container.CreateItemAsync(employee);

var sql = "SELECT * FROM employees e WHERE e.salary >= 150000";

var query = container.GetItemQueryIterator<Employee>(sql);

while (query.HasMoreResults)
{
    var response = await query.ReadNextAsync();
    foreach (var employee in response)
    {
        Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Salary}");
    }
}

Console.WriteLine("Done!");