using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Factories;

public class SaleItemFactoy : ISaleItemFactory
{
    private readonly IProductRepository _productRepository;

    public SaleItemFactoy(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<SaleItem> CreateAsync(Guid productId, int quantity, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        
        if (product is null)
            throw new DomainException("Product not found");
        
        return new SaleItem(product.Id, quantity, product.Price);
    }
}