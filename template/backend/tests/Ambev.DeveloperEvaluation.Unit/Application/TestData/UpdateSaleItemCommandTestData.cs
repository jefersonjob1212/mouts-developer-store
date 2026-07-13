using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public class UpdateSaleItemCommandTestData : Faker<UpdateSaleItemCommand>
{
    public UpdateSaleItemCommandTestData()
    {
        RuleFor(x => x.ProductId, _ => Guid.NewGuid());

        RuleFor(x => x.Quantity, f => f.Random.Int(1, 20));
    }
}