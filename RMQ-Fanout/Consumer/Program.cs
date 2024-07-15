using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

// Declaração de uma fila exclusiva
var queueName = channel.QueueDeclare().QueueName;

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
	var body = ea.Body.ToArray();
	var message = Encoding.UTF8.GetString(body);

	Console.WriteLine($" [x] Received {message}");

	Thread.Sleep(3 * 1000);

	Console.WriteLine(" [x] Done");

	channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};

channel.BasicConsume(
	queue: queueName,
	autoAck: false,
	consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
