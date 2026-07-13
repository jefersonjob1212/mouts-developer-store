using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Factories;

public interface ISaleItemFactory
{
    Task<SaleItem> CreateAsync(Guid productId, int quantity, CancellationToken cancellationToken);
}