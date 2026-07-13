using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientByEmail;

/// <summary>
/// Command for retrieving a client by Email
/// </summary>
public class GetClientByEmailCommand : IRequest<GetClientByEmailResult>
{
    /// <summary>
    /// The unique email of the client to retrieve
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Initialize a new instance of GetClientByEmailCommand
    /// </summary>
    /// <param name="email"></param>
    public GetClientByEmailCommand(string email)
    {
        Email = email;
    }
}