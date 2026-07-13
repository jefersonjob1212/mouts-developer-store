using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
{
    /// <summary>
    /// Initialize a new instance
    /// </summary>
    /// <param name="context"></param>
    public SaleRepository(DefaultContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a sale by sale number
    /// </summary>
    /// <param name="number"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Sale> GetByNumberAsync(long number, CancellationToken cancellationToken)
        => Query().FirstAsync(x => x.Number == number, cancellationToken);

    /// <summary>
    /// Retrieves sales with criteria filter and paginated
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Sale>> GetPaginatedAsync(SaleFilter filter, CancellationToken cancellationToken = default)
    {
        var sales = Query().Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Include(x => x.Client)
            .Include(x => x.Subsidiary)
            .AsQueryable();
        
        if (filter.Number.HasValue)
            sales = sales.Where(x => x.Number == filter.Number);

        if (filter.ClientId.HasValue)
            sales = sales.Where(x => x.ClientId == filter.ClientId);
        
        if (filter.SubsidiaryId.HasValue)
            sales = sales.Where(x => x.SubsidiaryId == filter.SubsidiaryId);
        
        return await sales
            .Skip((filter.PageIndex - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Count sales
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> CountAsync(SaleFilter filter, CancellationToken cancellationToken)
    {
        var sales = Query();
        
        if (filter.Number.HasValue)
            sales = sales.Where(x => x.Number == filter.Number);

        if (filter.ClientId.HasValue)
            sales = sales.Where(x => x.ClientId == filter.ClientId);
        
        if (filter.SubsidiaryId.HasValue)
            sales = sales.Where(x => x.SubsidiaryId == filter.SubsidiaryId);
        
        return await sales
            .CountAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a sale with items, clients and subsidiary
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default)
        => Query()
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Include(x => x.Client)
            .Include(x => x.Subsidiary)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    
    /// <summary>
    /// Retrieve a sale item by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SaleItem?> GetItemById(Guid id, CancellationToken cancellationToken)
        => await _context.Set<SaleItem>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    /// <summary>
    /// Update a sale item by ID
    /// </summary>
    /// <param name="saleItem"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<SaleItem> UpdateItemAsync(SaleItem saleItem, CancellationToken cancellationToken)
    {
        _context.SaleItems.Update(saleItem);
        return Task.FromResult(saleItem);
    }
}