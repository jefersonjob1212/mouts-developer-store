using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for create a new sale
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public long Number { get; set; }
    public DateTime Date { get; set; }
    public Guid ClientId { get; set; }
    public Guid SubsidiaryId { get; set; }
    public IList<CreateSaleItemCommand> Items { get; set; }
}