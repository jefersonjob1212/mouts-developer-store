using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Subsidiary : BaseEntity
{
    public string Cnpj { get; set; }
    public string LegalName { get; set; }
    public string TradeName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}