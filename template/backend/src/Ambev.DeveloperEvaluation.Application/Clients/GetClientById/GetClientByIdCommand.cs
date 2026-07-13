using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientById;

/// <summary>
/// Command for retrieving a client by ID
/// </summary>
public class GetClientByIdCommand : IRequest<GetClientByIdResult>
{
    /// <summary>
    /// The unique ID of the client to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initialize a new instance of GetClientByIdCommand
    /// </summary>
    /// <param name="id"></param>
    public GetClientByIdCommand(Guid id)
    {
        Id = id;
    }
}