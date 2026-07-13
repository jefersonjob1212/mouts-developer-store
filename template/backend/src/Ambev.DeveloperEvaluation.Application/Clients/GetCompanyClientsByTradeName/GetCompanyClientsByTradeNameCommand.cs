using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByTradeName;

/// <summary>
/// Command for retrieving a company client by trade name
/// </summary>
public class GetCompanyClientsByTradeNameCommand : IRequest<IEnumerable<GetCompanyClientsByTradeNameResult>>
{
    /// <summary>
    /// The trade name of the company client to retrieve
    /// </summary>
    public string TradeName { get; set; }

    /// <summary>
    /// Initialize a new instance of GetCompanyClientsByTradeNameCommand
    /// </summary>
    /// <param name="tradeName"></param>
    public GetCompanyClientsByTradeNameCommand(string tradeName)
    {
        TradeName = tradeName;
    }
}