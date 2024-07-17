using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

var logLevels = new List<string> { "system.error", "application.warning", "system.info", "application.warning", "application.info", "application.error" };

for (int i = 0; i < 6; i++)
{
	string message = $"Log message {i} - Log level: {logLevels[i]}";
	var body = Encoding.UTF8.GetBytes(message);

	channel.BasicPublish(
		exchange: "topic_logs",
		routingKey: logLevels[i],
		basicProperties: null,
		body: body);

	Console.WriteLine($"[x] Sent {message}");
}

Console.WriteLine("Press [Enter] to exit.");
Console.ReadLine();
