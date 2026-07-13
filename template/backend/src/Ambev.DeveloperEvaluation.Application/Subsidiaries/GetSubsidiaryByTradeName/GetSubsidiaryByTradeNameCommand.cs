using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByTradeName;

/// <summary>
/// Command for retrieving a subsidiary by CNPJ
/// </summary>
public class GetSubsidiaryByTradeNameCommand : IRequest<IEnumerable<GetSubsidiaryByTradeNameResult>>
{
    /// <summary>
    /// The unique CNPJ of the subsidiary to retrieve
    /// </summary>
    public string TradeName { get; set; }

    /// <summary>
    /// Initialize a new instance of GetCnpjByCommand
    /// </summary>
    /// <param name="tradeName"></param>
    public GetSubsidiaryByTradeNameCommand(string tradeName)
    {
        TradeName = tradeName;
    }
}