using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByName;

/// <summary>
/// Command for retrieving a company client by name
/// </summary>
public class GetIndividualClientByNameCommand : IRequest<IEnumerable<GetIndividualClientByNameResult>>
{
    /// <summary>
    /// The name of the company client to retrieve
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Initialize a new instance of GetIndividualClientByNameCommand
    /// </summary>
    /// <param name="name"></param>
    public GetIndividualClientByNameCommand(string name)
    {
        Name = name;
    }
}