using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository : IRepositoryBase<Product>
{
    /// <summary>
    /// Retrieves a product by code
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a product by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<IEnumerable<Product>> GetByNameAsync(string name);
}