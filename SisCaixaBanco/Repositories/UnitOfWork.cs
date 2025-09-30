using SisCaixaBanco.Data;
using SisCaixaBanco.Models;

namespace SisCaixaBanco.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IContaRepository Contas { get; }
        public IRepository<ContaLog> ContasLog { get; }
        public IRepository<Transferencia> Transferencias { get; }
        
        private readonly CaixaBancoDbContext _context;
        public UnitOfWork(CaixaBancoDbContext context,
            IContaRepository contaRepository,
            IRepository<Transferencia> transferenciaRepository,
            IRepository<ContaLog> contaLogRepository)
        {
            Contas = contaRepository;
            Transferencias = transferenciaRepository;
            ContasLog = contaLogRepository;
            _context = context;

        }
        public void Dispose()
        {
            _context.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
