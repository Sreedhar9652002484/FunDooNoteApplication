using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;

public class RabbitMQPublisher
{
    private readonly ConnectionFactory factory;

    public RabbitMQPublisher(ConnectionFactory _factory)
    {
        factory = _factory;
    }

    public void PublishMessage(string queueName, string message)
    {
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: messageBytes);
        }
    }
}
