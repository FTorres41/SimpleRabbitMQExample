using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Domain.Models
{
    public class Mensagem
    {
        public Mensagem()
        {

        }

        public Mensagem(Guid id, string texto, string identificador, string data)
        {
            Id = id;
            Texto = texto;
            Identificador = identificador;
            Data = data;
        }

        public Guid Id { get; set; }
        public string Texto { get; set; }
        public string Identificador { get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
