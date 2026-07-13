using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

public class CancelSaleResponse
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
    public IList<CancelSaleItemResponse> Items { get; set; }
}