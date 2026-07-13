using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public class CreateSaleHandlerTestData : Faker<CreateSaleCommand>
{
    public CreateSaleHandlerTestData()
    {
        RuleFor(x => x.Number, f => f.Random.Int(1, 1000));
        RuleFor(x => x.ClientId, f => Guid.NewGuid());
        RuleFor(x => x.SubsidiaryId, f => Guid.NewGuid());

        RuleFor(x => x.Items, f => new List<CreateSaleItemCommand>
        {
            new()
            {
                ProductId = Guid.NewGuid(),
                Quantity = 5
            }
        });
    }
}