using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByCnpj;

/// <summary>
/// Command for retrieving a subsidiary by CNPJ
/// </summary>
public class GetSubsidiaryByCnpjCommand : IRequest<GetSubsidiaryByCnpjResult>
{
    /// <summary>
    /// The unique CNPJ of the subsidiary to retrieve
    /// </summary>
    public string Cnpj { get; set; }

    /// <summary>
    /// Initialize a new instance of GetCnpjByCommand
    /// </summary>
    /// <param name="cnpj"></param>
    public GetSubsidiaryByCnpjCommand(string cnpj)
    {
        Cnpj = cnpj;
    }
}