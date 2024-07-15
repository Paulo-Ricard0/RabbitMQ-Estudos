using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

for (int i = 0; i < 10; i++)
{
	string message = $"Log message {i}";
	var body = Encoding.UTF8.GetBytes(message);

	channel.BasicPublish(
		exchange: "logs",
		routingKey: "",
		basicProperties: null,
		body: body);

	Console.WriteLine($"[x] Sent {message}");
}

Console.WriteLine("Press [Enter] to exit.");
Console.ReadLine();
