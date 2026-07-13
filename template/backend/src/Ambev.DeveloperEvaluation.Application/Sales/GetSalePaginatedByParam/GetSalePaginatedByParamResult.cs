using Ambev.DeveloperEvaluation.Application.Sales.GetSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;

/// <summary>
/// Response model for GetSalePaginatedByParamResult operation
/// </summary>
public class GetSalePaginatedByParamResult
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
    /// Total values of sale
    /// </summary>
    public decimal TotalValues { get; set; }

    /// <summary>
    /// Items of sale
    /// </summary>
    public IList<GetSaleItemResult> Items { get; set; }

    public int TotalCount { get; set; }
}