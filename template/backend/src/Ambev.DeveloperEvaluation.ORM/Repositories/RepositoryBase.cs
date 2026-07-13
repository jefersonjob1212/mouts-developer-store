using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IRepositoryBase using Entity Framework Core
/// </summary>
public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    protected readonly DefaultContext _context;

    public RepositoryBase(DefaultContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Retrieves an entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    /// <summary>
    /// Create an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    /// <summary>
    /// Update an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Delete an entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is not null)
        {
            _context.Set<T>().Remove(entity);
        }
    }

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        => Task.FromResult(Query().AsEnumerable());

    /// <summary>
    /// Retrieves queryable an entity
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> Query() => _context.Set<T>();
}