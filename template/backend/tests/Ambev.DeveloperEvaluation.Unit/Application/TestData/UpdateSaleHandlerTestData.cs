using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public class UpdateSaleHandlerTestData : Faker<UpdateSaleCommand>
{
    public UpdateSaleHandlerTestData()
    {
        RuleFor(x => x.Id, _ => Guid.NewGuid());

        RuleFor(x => x.Number, f => f.Random.Int(1, 9999));

        RuleFor(x => x.ClientId, _ => Guid.NewGuid());

        RuleFor(x => x.SubsidiaryId, _ => Guid.NewGuid());

        RuleFor(x => x.Items, 
            _ => new UpdateSaleItemCommandTestData().Generate(2));
    }
}