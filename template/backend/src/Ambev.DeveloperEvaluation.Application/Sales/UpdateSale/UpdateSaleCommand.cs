using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for update a sale
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; }
    public long Number { get; set; }
    public DateTime Date { get; set; }
    public Guid ClientId { get; set; }
    public Guid SubsidiaryId { get; set; }
    public SaleStatus Status { get; set; }
    public IList<UpdateSaleItemCommand> Items { get; set; }
}