using SisCaixaBanco.Models;

namespace SisCaixaBanco.Repositories
{
    public interface IContaRepository : IRepository<Conta>
    {
        Task<Conta> GetByDocumentoAsync(string documento);
        Task<IEnumerable<Conta>> SearchAsync(string nome, string documento);
    }

}
