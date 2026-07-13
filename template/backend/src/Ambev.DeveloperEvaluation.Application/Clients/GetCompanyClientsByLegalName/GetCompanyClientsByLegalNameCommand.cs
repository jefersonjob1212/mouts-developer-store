using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByLegalName;

/// <summary>
/// Command for retrieving a company client by legal name
/// </summary>
public class GetCompanyClientsByLegalNameCommand : IRequest<IEnumerable<GetCompanyClientsByLegalNameResult>>
{
    /// <summary>
    /// The legal name of the company client to retrieve
    /// </summary>
    public string LegalName { get; set; }

    /// <summary>
    /// Initialize a new instance of GetCompanyClientsByLegalNameCommand
    /// </summary>
    /// <param name="legalName"></param>
    public GetCompanyClientsByLegalNameCommand(string legalName)
    {
        LegalName = legalName;
    }
}