using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    /// <summary>
    /// Initialize a new instance
    /// </summary>
    /// <param name="context"></param>
    public ProductRepository(DefaultContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a product by code
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken)
        => Query().FirstOrDefaultAsync(p => p.Code == code, cancellationToken);

    /// <summary>
    /// Retrieves a product by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<IEnumerable<Product>> GetByNameAsync(string name)
        => Task.FromResult(Query().Where(p => p.Name.ToUpper().Contains(name.ToUpper())).AsEnumerable());
}