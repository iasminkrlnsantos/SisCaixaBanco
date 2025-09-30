using Microsoft.EntityFrameworkCore;
using SisCaixaBanco.DTO;
using SisCaixaBanco.Models;
using SisCaixaBanco.Repositories;
using SisCaixaBanco.Common.Enums;
using Microsoft.Extensions.Localization;
using SisCaixaBanco.Resources;

namespace SisCaixaBanco.Services
{
    public class ContaService : IContaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContaRepository _contaRepository;

        public ContaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ContaService(IContaRepository contaRepository, IUnitOfWork unitOfWork)
        {
            _contaRepository = contaRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Cria uma nova conta. Lança InvalidOperationException se documento já existir.
        /// </summary>
        public async Task<Conta> CriarContaAsync(ContaCreateDTO dto)
        {
            var exist = await _unitOfWork.Contas.GetByDocumentoAsync(dto.Documento);
            if (exist != null)
                throw new Exception(GlobalResource.DocumentoUnicoError);

            var conta = new Conta
            {
                NomeCliente = dto.Nome,
                Documento = dto.Documento
            };

            await _unitOfWork.Contas.AddAsync(new Conta
            {
                NomeCliente = dto.Nome,
                Documento = dto.Documento
            });
            await _unitOfWork.SaveChangesAsync();

            return conta;
        }
        /// <summary>
        /// Busca contas filtrando por nome (contains) e/ou documento (equals).
        /// </summary>
        public async Task<IEnumerable<Conta>> ListarContasAsync(string? nome, string? documento)
        {
            return await _unitOfWork.Contas.SearchAsync(nome, documento);
        }

        /// <summary>
        /// Obtém uma conta por documento. Retorna null se não encontrada.
        /// </summary>
        public async Task<Conta> ObterPorDocumentoAsync(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento)) return null;
            return await _unitOfWork.Contas.GetByDocumentoAsync(documento);
        }

        /// <summary>
        /// Inativa a conta pelo documento e registra um ContaLog.
        /// </summary>
        public async Task InativarContaPorDocumentoAsync(string documento)
        {

            var conta = await _unitOfWork.Contas.GetByDocumentoAsync(documento) ?? throw new Exception(GlobalResource.ContaNotFoundError);
            if (conta.Status == StatusConta.Inativa)
                return;  

            conta.Status = StatusConta.Inativa;
            _unitOfWork.Contas.Update(conta);

            await _unitOfWork.ContasLog.AddAsync(new ContaLog
            {
                IdContaBancaria = conta.Id,
                NumeroDocumento = conta.Documento,
                DataDesativacao = DateTime.Now,
                UsuarioResponsavel = GlobalResource.User
            });

            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Transferencia entre contas
        /// Saldo da conta de origem debitado e saldo da conta de destino creditado
        /// </summary>
        public async Task TransferirAsync(TransferenciaDTO dto)
        {

            var contaOrigem = await _unitOfWork.Contas.GetByDocumentoAsync(dto.DocumentoOrigem) ?? throw new Exception(String.Format(GlobalResource.ContaNotFoundError,"Origem"));
            var contaDestino = await _unitOfWork.Contas.GetByDocumentoAsync(dto.DocumentoDestino) ?? throw new Exception(String.Format(GlobalResource.ContaNotFoundError, "Destino"));
            
            if (contaOrigem.Status == StatusConta.Inativa) throw new Exception(String.Format(GlobalResource.ContaInativaError, "Origem"));
            if (contaDestino.Status == StatusConta.Inativa) throw new Exception(String.Format(GlobalResource.ContaInativaError, "Destino"));

            contaOrigem.Debitar(dto.Valor);
            contaDestino.Creditar(dto.Valor);

            _unitOfWork.Contas.Update(contaOrigem);
            _unitOfWork.Contas.Update(contaDestino);


            var transferencia = new Transferencia
            {
                IdContaOrigem = contaOrigem.Id,
                IdContaDestino = contaDestino.Id,
                Valor = dto.Valor,
                Data = DateTime.Now
            };

            await _unitOfWork.Transferencias.AddAsync(transferencia);

            await _unitOfWork.SaveChangesAsync();
        }

    }
}
