using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Response model for CreateSaleResult operation
/// </summary>
public class CancelSaleResult
{
    /// <summary>
    /// Unique identifier of sale
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Number of sale
    /// </summary>
    public long Number { get; set; }
    
    /// <summary>
    /// Date of sale
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Client name of sale
    /// </summary>
    public string ClientName { get; set; }
    
    /// <summary>
    /// Subsidiary name of sale
    /// </summary>
    public string SubsidiaryName { get; set; }
    
    /// <summary>
    /// Sale status
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Items of sale
    /// </summary>
    public IList<CancelSaleItemResult> Items { get; set; }
}