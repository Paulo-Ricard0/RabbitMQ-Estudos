﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
	byte[] body = ea.Body.ToArray();
	var message = Encoding.UTF8.GetString(body);
	Console.WriteLine($" [x] Received {message}");

	Thread.Sleep(3 * 1000);

	Console.WriteLine(" [x] Done");

	channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};

channel.BasicConsume(
	queue: "hello",
	autoAck: false,
	consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
