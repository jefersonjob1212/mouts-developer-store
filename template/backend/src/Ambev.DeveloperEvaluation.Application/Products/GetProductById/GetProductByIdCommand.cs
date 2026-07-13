using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById;

/// <summary>
/// Command for retrieving a product by their ID
/// </summary>
public class GetProductByIdCommand : IRequest<GetProductByIdResult>
{
    /// <summary>
    /// The unique identifier of the product to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of GetProductByIdCommand
    /// </summary>
    /// <param name="id"></param>
    public GetProductByIdCommand(Guid id)
    {
        Id = id;
    }
}