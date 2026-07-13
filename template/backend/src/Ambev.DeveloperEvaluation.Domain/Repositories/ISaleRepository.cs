using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository base for Sale entity operations
/// </summary>
public interface ISaleRepository : IRepositoryBase<Sale>
{
    /// <summary>
    /// Retrieves a sale by sale number
    /// </summary>
    /// <param name="number"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Sale> GetByNumberAsync(long number, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves sale with pagination
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Sale>> GetPaginatedAsync(SaleFilter filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Count sales
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> CountAsync(SaleFilter filter, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a sale with items
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieve a sale item by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SaleItem?> GetItemById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update a sale item by ID
    /// </summary>
    /// <param name="saleItem"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SaleItem> UpdateItemAsync(SaleItem saleItem, CancellationToken cancellationToken);
}