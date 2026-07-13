using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace  Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SaleTestData : Faker<Sale>
{
    public SaleTestData()
    {
        CustomInstantiator(f =>
            new Sale(
                f.Random.Long(1, 999999),
                f.Date.Past(),
                Guid.NewGuid(),
                Guid.NewGuid()));
    }
}