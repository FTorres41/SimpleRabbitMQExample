using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Domain.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            Console.WriteLine("Quantos producers deseja iniciar?");
            var quantidade = int.Parse(Console.ReadLine());

            for (int i = 0; i < quantidade; i++)
            {
                using (var process1 = new Process())
                {
                    process1.StartInfo.FileName = @"..\..\..\..\RabbitMQ.Producer\bin\Debug\netcoreapp3.1\RabbitMQ.Producer.exe";
                    process1.Start();
                }
            }

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "messageQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var mensagem = JsonSerializer.Deserialize<Mensagem>(Encoding.UTF8.GetString(body));

                    Console.WriteLine(" [x] Recebido {0}", mensagem.ToString());
                };
                channel.BasicConsume(queue: "messageQueue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
