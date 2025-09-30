using SisCaixaBanco.Models;
using SisCaixaBanco.Repositories;

namespace SisCaixaBanco.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Registra Transferência.
        /// </summary>
        public async Task RegistrarAsync(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            var transferencia = new Transferencia()
            {
                IdContaOrigem = contaOrigem.Id,
                IdContaDestino = contaDestino.Id,
                Valor = valor,
                Data = DateTime.Now
            };

            await _unitOfWork.Transferencias.AddAsync(transferencia);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
