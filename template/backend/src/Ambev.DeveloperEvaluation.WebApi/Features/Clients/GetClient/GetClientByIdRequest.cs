namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;

/// <summary>
/// Request for retrieve a client by ID
/// </summary>
public class GetClientByIdRequest
{
    /// <summary>
    /// Unique identifier of client
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initialize new instance of GetClientByIdRequest
    /// </summary>
    /// <param name="id"></param>
    public GetClientByIdRequest(Guid id)
    {
        Id = id;
    }
}