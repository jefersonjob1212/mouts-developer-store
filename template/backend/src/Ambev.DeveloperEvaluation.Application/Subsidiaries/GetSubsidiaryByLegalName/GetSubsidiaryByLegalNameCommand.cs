using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByLegalName;

/// <summary>
/// Command for retrieving a subsidiary by CNPJ
/// </summary>
public class GetSubsidiaryByLegalNameCommand : IRequest<IEnumerable<GetSubsidiaryByLegalNameResult>>
{
    /// <summary>
    /// The unique CNPJ of the subsidiary to retrieve
    /// </summary>
    public string LegalName { get; set; }

    /// <summary>
    /// Initialize a new instance of GetCnpjByCommand
    /// </summary>
    /// <param name="legalName"></param>
    public GetSubsidiaryByLegalNameCommand(string legalName)
    {
        LegalName = legalName;
    }
}