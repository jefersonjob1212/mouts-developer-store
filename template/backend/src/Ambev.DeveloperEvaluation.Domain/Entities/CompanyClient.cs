using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain;

public class CompanyClient : Client
{
    public string Cnpj { get; set; }
    public string LegalName { get; set; }
    public string TradeName { get; set; }
}