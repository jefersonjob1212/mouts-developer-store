using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for create a new sale
/// </summary>
public class CancelSaleCommand : IRequest<CancelSaleResult>
{
    public Guid Id { get; set; }
}