using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.SearchClients;

/// <summary>
/// Command for retrieving a company client by trade name
/// </summary>
public class SearchClientsCommand : IRequest<IEnumerable<SearchClientsResult>>
{
    /// <summary>
    /// The trade name of the company client to retrieve
    /// </summary>
    public string Term { get; set; }

    /// <summary>
    /// Initialize a new instance of SearchClientsCommand
    /// </summary>
    /// <param name="term"></param>
    public SearchClientsCommand(string term)
    {
        Term = term;
    }
}