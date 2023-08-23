using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using MassTransit;
using System;
using FunDooNoteSubscriber.Interface;
using NoteAppSubscriber;
using NoteAppSubscriber.Services;

namespace FunDooNoteSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("C:\\Users\\acer\\source\\repos\\FunDooNoteSubscriber\\FunDooNoteSubscriber\\appsettings.json", optional: false)
                .Build();

            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQSettings:HostName"],
                UserName = configuration["RabbitMQSettings:UserName"],
                Password = configuration["RabbitMQSettings:Password"]
            };

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(configuration["RabbitMQSettings:HostUri"]), h =>
                {
                    h.Username(configuration["RabbitMQSettings:UserName"]);
                    h.Password(configuration["RabbitMQSettings:Password"]);
                });

                // Automatically register the consumer
                cfg.ReceiveEndpoint("User-Registration-Queue", e =>
                {
                    // Automatically register the consumer using the DI container
                    e.Consumer<UserRegistrationEmailSubscriber>();
                });
            });

            var subscriber = new RabbitMQSubscriber(factory, configuration, busControl);
            subscriber.ConsumeMessages();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}