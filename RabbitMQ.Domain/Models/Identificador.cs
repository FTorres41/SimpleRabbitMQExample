using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Domain.Models
{
    public class Identificador
    {
        public string Nome { get => this.CriarNome(); }

        internal string CriarNome()
        {
            int comprimento = 7;

            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            char letra;

            for (int i = 0; i < comprimento; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letra = Convert.ToChar(shift + 65);
                sb.Append(letra);
            }

            return sb.ToString();
        }
    }
}
