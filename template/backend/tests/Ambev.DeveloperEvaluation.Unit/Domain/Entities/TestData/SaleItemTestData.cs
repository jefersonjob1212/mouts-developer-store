using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SaleItemTestData : Faker<SaleItem>
{
    public SaleItemTestData()
    {
        CustomInstantiator(f =>
            new SaleItem(
                Guid.NewGuid(),
                f.Random.Int(1, 20),
                f.Random.Int(1, 10)));
    }
}