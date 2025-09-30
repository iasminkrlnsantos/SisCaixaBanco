using System.ComponentModel.DataAnnotations.Schema;

namespace SisCaixaBanco.Models
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int IdContaOrigem { get; set; }
        public int IdContaDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public Conta ContaOrigem { get; set; }
        public Conta ContaDestino { get; set; }

    }
}
