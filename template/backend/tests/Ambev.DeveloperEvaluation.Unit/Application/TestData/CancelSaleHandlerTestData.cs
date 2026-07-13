using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public class CancelSaleHandlerTestData : Faker<CancelSaleCommand>
{
    public CancelSaleHandlerTestData()
    {
        RuleFor(x => x.Id, _ => Guid.NewGuid());
    }
}