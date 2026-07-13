using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientByCnpj;

/// <summary>
/// Command for retrieving a company client by CNPJ
/// </summary>
public class GetCompanyClientByCnpjCommand : IRequest<GetCompanyClientByCnpjResult>
{
    /// <summary>
    /// The unique CNPJ of the company client to retrieve
    /// </summary>
    public string Cnpj { get; set; }

    /// <summary>
    /// Initialize a new instance of GetCompanyClientByCnpjCommand
    /// </summary>
    /// <param name="cnpj"></param>
    public GetCompanyClientByCnpjCommand(string cnpj)
    {
        Cnpj = cnpj;
    }
}