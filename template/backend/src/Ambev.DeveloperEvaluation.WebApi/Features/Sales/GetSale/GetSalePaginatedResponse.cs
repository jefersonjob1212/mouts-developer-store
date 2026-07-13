namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSalePaginatedResponse
{
    public IEnumerable<GetSaleResponse> SaleList { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}