using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCode;

/// <summary>
/// Command for retrieving a products by name
/// </summary>
public class GetProductByCodeCommand : IRequest<GetProductByCodeResult>
{
    /// <summary>
    /// The unique code of the product to retrieve
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Initializes a new instance of GetProductByIdCommand
    /// </summary>
    /// <param name="code"></param>
    public GetProductByCodeCommand(string code)
    {
        Code = code;
    }
}