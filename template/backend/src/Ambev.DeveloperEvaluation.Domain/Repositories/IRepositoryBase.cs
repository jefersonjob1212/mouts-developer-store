using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface base for commons database commands
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryBase<T> where T : BaseEntity
{
    /// <summary>
    /// Retrieves an entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Create an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// Delete an entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Query dbset
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> Query();
}