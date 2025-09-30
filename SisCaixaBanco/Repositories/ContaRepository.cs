using Microsoft.EntityFrameworkCore;
using SisCaixaBanco.Data;
using SisCaixaBanco.DTO;
using SisCaixaBanco.Models;

namespace SisCaixaBanco.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(CaixaBancoDbContext context) : base(context)
        {
        }

        public async Task<Conta> GetByDocumentoAsync(string documento)
        {
            return await _dbSet.Where(c => c.Documento.Equals(documento)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Conta>> SearchAsync(string nome, string documento)
        {
            return await _dbSet.AsNoTracking().Where(c => c.Documento.Equals(documento) || c.NomeCliente.Contains(nome)).ToListAsync();
        }
    }
}
