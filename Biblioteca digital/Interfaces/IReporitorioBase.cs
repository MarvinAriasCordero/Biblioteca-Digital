using Biblioteca_digital.Dtos.books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Biblioteca_digital.Interfaces
{
    public interface IReporitorioBase<T> where T : class
    {
        /// <summary>
        /// listas de base de datos
        /// </summary>
        public DbSet<T> Set { get; }


        /// <summary>
        /// Crear un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task CreateAsync(T entity);


        /// <summary>
        /// actualizar un registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(T entity);


        /// <summary>
        /// eliminar un reguistro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task DeleteAsync(T entity);


        /// <summary>
        /// mostrar todos los registros 
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> GetAllAsync();


        /// <summary>
        /// obtener por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T?> GetByIdAsync(Guid id);


        /// <summary>
        /// busqueda por filtro o query
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="inclideProperties"></param>
        /// <returns></returns>
        public Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// resultado paginado de la base de datos 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public  Task<RepuestaPaginada<T>> GetPagedData(int pageNumber, int pageSize);

        /// <summary>
        /// guarda los cambios de la base de datos y cierra la conexion.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
