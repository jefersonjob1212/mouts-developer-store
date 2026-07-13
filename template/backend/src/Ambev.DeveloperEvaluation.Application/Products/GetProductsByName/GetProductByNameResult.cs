namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByName;

/// <summary>
/// Response model for GetProductByIdResult operation
/// </summary>
public class GetProductByNameResult
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