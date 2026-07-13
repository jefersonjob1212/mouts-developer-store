namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;

public class GetSalePaginatedResult
{
    public IEnumerable<GetSalePaginatedByParamResult> SaleList { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}