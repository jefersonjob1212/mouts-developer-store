namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public long Number { get; set; }
    public DateTime Date { get; set; }
    public Guid ClientId { get; set; }
    public Guid SubsidiaryId { get; set; }
    public IList<CreateSaleItemRequest> Items { get; set; }
}