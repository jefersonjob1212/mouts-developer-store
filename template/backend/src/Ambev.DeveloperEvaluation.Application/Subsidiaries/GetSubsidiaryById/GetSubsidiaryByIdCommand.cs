using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryById;

/// <summary>
/// Command for retrieving a subsidiary by ID
/// </summary>
public class GetSubsidiaryByIdCommand : IRequest<GetSubsidiaryByIdResult>
{
    /// <summary>
    /// The unique ID of the subsidiary to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initialize a new instance of GetIdByCommand
    /// </summary>
    /// <param name="id"></param>
    public GetSubsidiaryByIdCommand(Guid id)
    {
        Id = id;
    }
}