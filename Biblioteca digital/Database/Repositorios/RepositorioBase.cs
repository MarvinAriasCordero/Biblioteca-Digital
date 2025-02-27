using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Biblioteca_digital.Database;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Dtos.books;

namespace Biblioteca_digital.Database.Repositorios
{
    public class RepositorioBase<T> : IReporitorioBase<T> where T : class
    {
        private readonly BibliotecaDbContext _dbContext;

        public RepositorioBase(BibliotecaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<T> Set => _dbContext.Set<T>();

        public async Task CreateAsync(T entity)
        {

            var result = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            var result = _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<RepuestaPaginada<T>> GetPagedData( int pageNumber, int pageSize)
        {
            try{


                var query = _dbContext.Set<T>().AsQueryable<T>();
                var totalRecords = await query.CountAsync(); // Total de registros
                if(totalRecords == 0) return new RepuestaPaginada<T>
                {
                    Items = [],
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
                };

                if (pageNumber > totalRecords) pageNumber = totalRecords;
                    var items = await query.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync(); // Obtener la página

                return new RepuestaPaginada<T>
                {
                    Items = items,
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
                };

            }
            catch (Exception ex) {
                throw new Exception($"Execpcion: {ex.Message}");
            }
                
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null,
                 Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                 params Expression<Func<T, object>>[] includeProperties)
        {

            var query = _dbContext.Set<T>().AsQueryable<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }

            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();


            throw new NotImplementedException();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(new object[] { id });
        }


        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
