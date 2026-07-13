namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Request for retrieves sales paginated by param
/// </summary>
public class GetSalePaginatedByParamRequest
{
    /// <summary>
    /// The unique number of the sale to retrieve
    /// </summary>
    public long? Number { get; set; }

    /// <summary>
    /// The client ID of the sale to retrieve
    /// </summary>
    public Guid? ClientId { get; set; }

    /// <summary>
    /// The subsidiary ID of the sale to retrieve
    /// </summary>
    public Guid? SubsidiaryId { get; set; }
    
    /// <summary>
    /// The page for retrieve a list of sales
    /// </summary>
    public int PageIndex { get; set; }
    
    /// <summary>
    /// The size for limit retrieves a list of sales
    /// </summary>
    public int PageSize { get; set; }
}