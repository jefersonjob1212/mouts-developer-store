using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Response model for CreateSaleItemResult operation
/// </summary>
public class UpdateSaleItemResult
{
    /// <summary>
    /// Product name of sale item
    /// </summary>
    public string ProductName { get; set; }
    
    /// <summary>
    /// Price of prodcut in sale item
    /// </summary>
    public decimal UnitPrice { get; set; }
    
    /// <summary>
    /// Quantity of sale item
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Discount of item
    /// </summary>
    public decimal Discount { get; set; }
    
    /// <summary>
    /// Total of item
    /// </summary>
    public decimal Total { get; set; }
    
    /// <summary>
    /// Status item
    /// </summary>
    public SaleItemStatus Status { get; set; }
}