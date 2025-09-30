using System.ComponentModel.DataAnnotations;
using SisCaixaBanco.Common;

namespace SisCaixaBanco.DTO
{
    public class ContaDTO
    {
        public string NomeCliente { get; set; }
        public string Documento { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataAbertura { get; set; }
        public string StatusDaConta { get; set; }

    }
}
