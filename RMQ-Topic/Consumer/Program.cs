using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

var queueName = channel.QueueDeclare().QueueName;

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

Console.WriteLine("Which logs to access?");
Console.WriteLine("1- System logs");
Console.WriteLine("2- Application logs");
var key = Console.ReadLine();

var bindingKey = "";

if (key == "1")
{
	bindingKey = "system.*";
}
else if (key == "2")
{
	bindingKey = "application.*";
}

channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: bindingKey);

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

channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

Console.WriteLine(" Press [Enter] to exit.");
Console.ReadLine();
