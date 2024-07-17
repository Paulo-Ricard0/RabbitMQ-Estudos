using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

var guid = Guid.NewGuid();
var errorQueueName = $"error_queue-{guid}";
var generalQueueName = $"general_queue-{guid}";

channel.QueueDeclare(queue: errorQueueName);
channel.QueueDeclare(queue: generalQueueName);

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

channel.QueueBind(queue: errorQueueName, exchange: "direct_logs", routingKey: "error");
channel.QueueBind(queue: generalQueueName, exchange: "direct_logs", routingKey: "warning");
channel.QueueBind(queue: generalQueueName, exchange: "direct_logs", routingKey: "info");

Console.WriteLine("Which log queue to consume?");
Console.WriteLine("To consume the general queue type: general");
Console.WriteLine("To consume the error queue type: error");

var queueName = Console.ReadLine();

if (queueName == "error")
{
	Console.Clear();
	Console.WriteLine(" [*] Waiting for messages.");

	var errorConsumer = new EventingBasicConsumer(channel);
	errorConsumer.Received += (model, ea) =>
	{
		var body = ea.Body.ToArray();
		var message = Encoding.UTF8.GetString(body);

		Console.WriteLine($" [x] {errorQueueName} Received: {message}");

		Thread.Sleep(3 * 1000);

		Console.WriteLine(" [x] Done  \n");

		channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
	};

	channel.BasicConsume(queue: errorQueueName, autoAck: false, consumer: errorConsumer);
}
else if (queueName == "general")
{
	Console.Clear();
	Console.WriteLine(" [*] Waiting for messages.");

	var generalConsumer = new EventingBasicConsumer(channel);
	generalConsumer.Received += (model, ea) =>
	{
		var body = ea.Body.ToArray();
		var message = Encoding.UTF8.GetString(body);

		Console.WriteLine($" [x] {generalQueueName} Received: {message}");

		Thread.Sleep(3 * 1000);

		Console.WriteLine(" [x] Done \n");

		channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
	};

	channel.BasicConsume(queue: generalQueueName, autoAck: false, consumer: generalConsumer);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
