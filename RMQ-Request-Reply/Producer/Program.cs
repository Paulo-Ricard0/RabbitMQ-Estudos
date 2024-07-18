using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
var replyQueueName = channel.QueueDeclare().QueueName;

var correlationId = Guid.NewGuid().ToString();
string response = "";

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
	var body = ea.Body.ToArray();
	var message = Encoding.UTF8.GetString(body);

	if (ea.BasicProperties.CorrelationId == correlationId)
	{
		response = message;
		channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
	}
};
channel.BasicConsume(queue: replyQueueName, autoAck: false, consumer: consumer);

var props = channel.CreateBasicProperties();
props.CorrelationId = correlationId;
props.ReplyTo = replyQueueName;

var message = Encoding.UTF8.GetBytes("Hello, Consumer!");

channel.BasicPublish(exchange: "", routingKey: "rpc_queue", basicProperties: props, body: message);

Console.WriteLine("[x] Waiting for a reply...");
while (response.Length == 0)
{
	Thread.Sleep(100);
}

Console.WriteLine($"[x] {response}");
Console.WriteLine("Press [Enter] to exit.");
Console.ReadLine();
