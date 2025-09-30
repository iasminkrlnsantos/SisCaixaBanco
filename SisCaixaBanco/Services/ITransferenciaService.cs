using SisCaixaBanco.Models;

namespace SisCaixaBanco.Services
{
    public interface ITransferenciaService
    {
        public interface ITransferenciaService
        {
            Task RegistrarAsync(Conta contaOrigem, Conta contaDestino, decimal valor);
        }
    }
}
