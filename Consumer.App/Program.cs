// See https://aka.ms/new-console-template for more information
using Consumer.App;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

Console.WriteLine("Consumer");

var connectionFactory = new ConnectionFactory();
//connectionFactory.Uri = new Uri("amqps://udijpcsq:rjruXr0KRc3P27pfV6Oygxq5WvL0eY3V@shark.rmq.cloudamqp.com/udijpcsq");
connectionFactory.HostName = "localhost";
var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.BasicQos(0, 20, false);

var consumer= new EventingBasicConsumer(channel);

consumer.Received += (object? sender, BasicDeliverEventArgs? args) =>
{
	
        var userCreatedEventAsJson = Encoding.UTF8.GetString(args.Body.ToArray());

        var userCreatedEvent = JsonSerializer.Deserialize<UserCreatedEvent>(userCreatedEventAsJson);

        Console.WriteLine($"Gelen mesaj: id:{userCreatedEvent?.Id}, name:{userCreatedEvent?.Name}, email:{userCreatedEvent?.Email}");
		channel.BasicAck(args.DeliveryTag,true);
  
 

};
channel.BasicConsume("myqueue",false, consumer);

Console.ReadLine();