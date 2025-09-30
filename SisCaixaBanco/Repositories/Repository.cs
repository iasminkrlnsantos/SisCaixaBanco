using Microsoft.EntityFrameworkCore;
using SisCaixaBanco.Data;

namespace SisCaixaBanco.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CaixaBancoDbContext _context;
        protected readonly DbSet<T> _dbSet;


        public Repository(CaixaBancoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public void Update(T entity) => _dbSet.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}

