using Microsoft.Extensions.Configuration;
//using NoteAppSubscriber.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
//using CommonLayer.Model; // Assuming UserRegistrationMessage is defined here
using System;
using System.Text;
using MassTransit;
//using CommonLayer.Model;
using FunDooNoteSubscriber.Models;
using FunDooNoteSubscriber.Interface;

namespace NoteAppSubscriber
{
    public class RabbitMQSubscriber : IRabbitMQSubscriber
    {
        private readonly ConnectionFactory factory;
        private readonly IConfiguration configuration;
        private readonly IBusControl _busControl; // Add this field to inject MassTransit bus

        public RabbitMQSubscriber(ConnectionFactory _factory, IConfiguration _configuration, IBusControl busControl)
        {
            factory = _factory;
            configuration = _configuration;
            _busControl = busControl; // Inject the MassTransit bus

            // Start consuming messages
            ConsumeMessages();
        }

        public void ConsumeMessages()
        {
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("Connection to RabbitMQ server established");

                using (var channel = connection.CreateModel())
                {
                    var queueName = "User-Registration-Queue";
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        // Send the received email to the UserRegistrationEmailSubscriber consumer
                        await _busControl.Publish<UserRegistrationMessage>(new
                        {
                            Email = message
                        });
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                }
            }
        }
    }
}
