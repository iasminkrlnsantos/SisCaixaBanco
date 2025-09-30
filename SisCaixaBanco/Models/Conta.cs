using System.ComponentModel.DataAnnotations;
using SisCaixaBanco.DTO;
using SisCaixaBanco.Common.Enums;
using SisCaixaBanco.Resources;

namespace SisCaixaBanco.Models
{
    public class Conta
    { 
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Documento { get; set; }

        public decimal Saldo { get; set; }
        public DateTime DataAbertura { get; set; }
        public StatusConta Status { get; set; }

        public Conta()
        {
            Saldo = 1000;
            DataAbertura = DateTime.Now;
            Status = StatusConta.Ativa;
        }
        public ICollection<Transferencia> TransferenciasOrigem { get; set; }
        public ICollection<Transferencia> TransferenciasDestino { get; set; }

        public void Debitar(decimal valor)
        {
            if (valor <= 0)
                throw new InvalidOperationException(String.Format(GlobalResource.TransferenciaValorMinimoError, "débito"));

            if (Saldo < valor)
                throw new InvalidOperationException(GlobalResource.SaldoInsuficienteError);

            Saldo -= valor;
        }

        public void Creditar(decimal valor)
        {
            if (valor <= 0)
                throw new InvalidOperationException(String.Format(GlobalResource.TransferenciaValorMinimoError, "crédito"));

            Saldo += valor;
        }
    }
}
