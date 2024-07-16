using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

var logLevels = new List<string> { "error", "warning", "info", "warning", "info", "error" };

for (int i = 0; i < 6; i++)
{
	string message = $"Log message {i} - Log level: {logLevels[i]}";
	var body = Encoding.UTF8.GetBytes(message);

	channel.BasicPublish(
		exchange: "direct_logs",
		routingKey: $"{logLevels[i]}",
		basicProperties: null,
		body: body);

	Console.WriteLine($"[x] Sent {message}");
}

Console.WriteLine("Press [Enter] to exit.");
Console.ReadLine();
