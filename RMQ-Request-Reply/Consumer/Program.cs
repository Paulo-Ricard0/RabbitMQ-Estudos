using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
	var body = ea.Body.ToArray();
	var props = ea.BasicProperties;
	var replyProps = channel.CreateBasicProperties();
	replyProps.CorrelationId = props.CorrelationId;

	var message = Encoding.UTF8.GetString(body);
	var response = $"Response to '{message}': Hello, Producer!";

	var responseBytes = Encoding.UTF8.GetBytes(response);

	Console.WriteLine($" [x] Received {message}");

	Thread.Sleep(3 * 1000);

	Console.WriteLine(" [x] Done");

	channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
	channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};
channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);

Console.WriteLine(" [x] Awaiting RPC requests");
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
