namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Request for retrieves sale by id
/// </summary>
public class GetSaleByIdRequest
{
    /// <summary>
    /// The unique ID of sale
    /// </summary>
    public Guid Id { get; set; }
}