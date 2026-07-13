using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;

/// <summary>
/// Validator for GetClientByIdRequest
/// </summary>
public class GetClientByIdRequestValidator : AbstractValidator<GetClientByIdRequest>
{
    /// <summary>
    /// Initialize a new instance of GetClientByIdRequestValidator
    /// </summary>
    public GetClientByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}