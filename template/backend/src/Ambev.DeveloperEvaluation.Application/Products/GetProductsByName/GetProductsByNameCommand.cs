using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByName;

/// <summary>
/// Command for retrieving a products by name
/// </summary>
public class GetProductsByNameCommand : IRequest<IEnumerable<GetProductByNameResult>>
{
    /// <summary>
    /// The unique identifier of the product to retrieve
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Initializes a new instance of GetProductByIdCommand
    /// </summary>
    /// <param name="name"></param>
    public GetProductsByNameCommand(string name)
    {
        Name = name;
    }
}