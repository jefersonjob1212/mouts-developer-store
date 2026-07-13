namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public long Number { get; set; }
    public DateTime Date { get; set; }
    public Guid ClientId { get; set; }
    public Guid SubsidiaryId { get; set; }
    public IList<UpdateSaleItemRequest> Items { get; set; }
}