using SisCaixaBanco.DTO;
using SisCaixaBanco.Models;

namespace SisCaixaBanco.Services
{
    public interface IContaService
    {
        Task<Conta> CriarContaAsync(ContaCreateDTO dto);
        Task<IEnumerable<Conta>> ListarContasAsync(string? nome, string? documento);
        Task<Conta> ObterPorDocumentoAsync(string documento);
        Task InativarContaPorDocumentoAsync(string documento);
        Task TransferirAsync(TransferenciaDTO dto);
    }
}
