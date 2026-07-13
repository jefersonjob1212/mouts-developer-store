namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Response model for GetProductResponse operation
/// </summary>
public class GetProductResponse
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The name of product
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The description of product
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// The unique code of product
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// The price of product
    /// </summary>
    public decimal Price { get; set; }
}