using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
	queue: "hello",
	durable: true,
	exclusive: false,
	autoDelete: false,
	arguments: null);

for (int i = 0; i < 10; i++)
{
	string message = $"Message {i}";
	var body = Encoding.UTF8.GetBytes(message);

	var properties = channel.CreateBasicProperties();
	properties.Persistent = true;

	channel.BasicPublish(
		exchange: "",
		routingKey: "hello",
		basicProperties: properties,
		body: body);

	Console.WriteLine($"[x] Sent {message}");
}

Console.WriteLine("Press [Enter] to exit.");
Console.ReadLine();
