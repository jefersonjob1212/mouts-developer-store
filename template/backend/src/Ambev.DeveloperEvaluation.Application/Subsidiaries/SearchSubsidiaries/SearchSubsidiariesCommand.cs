using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.SearchSubsidiaries;

/// <summary>
/// Command for retrieving a subsidiary by trade name
/// </summary>
public class SearchSubsidiariesCommand : IRequest<IEnumerable<SearchSubsidiariesResult>>
{
    /// <summary>
    /// The trade name of the subsidiary to retrieve
    /// </summary>
    public string Term { get; set; }

    /// <summary>
    /// Initialize a new instance of SearchClientsCommand
    /// </summary>
    /// <param name="term"></param>
    public SearchSubsidiariesCommand(string term)
    {
        Term = term;
    }
}