using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

/// <summary>
/// Command for retrieving a subsidiary by ID
/// </summary>
public class GetSaleByIdCommand : IRequest<GetSaleByIdResult>
{
    /// <summary>
    /// The unique ID of the subsidiary to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initialize a new instance of GetIdByCommand
    /// </summary>
    /// <param name="id"></param>
    public GetSaleByIdCommand(Guid id)
    {
        Id = id;
    }
}