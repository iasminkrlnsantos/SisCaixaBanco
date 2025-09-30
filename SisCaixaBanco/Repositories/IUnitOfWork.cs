using SisCaixaBanco.Models;

namespace SisCaixaBanco.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IContaRepository Contas { get; }
        IRepository<ContaLog> ContasLog { get; }
        IRepository<Transferencia> Transferencias { get; }

        Task<int> SaveChangesAsync();
    }
}
