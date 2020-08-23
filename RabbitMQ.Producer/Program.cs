using RabbitMQ.Client;
using RabbitMQ.Domain.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Timers;

namespace RabbitMQExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var identificador = new Identificador();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "messageQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                EnviarMensagens(identificador, channel);
            }

            Console.ReadLine();
        }

        private static void EnviarMensagens(Identificador identificador, IModel channel)
        {
            var ativo = true;

            while (ativo)
            {
                var mensagem = new Mensagem(Guid.NewGuid(), "Hello World", identificador.Nome, DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")).ToString();
                var body = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(exchange: "",
                                     routingKey: "messageQueue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Enviado: {0}", mensagem);

                Thread.Sleep(5000);
            }
        }
    }
}
