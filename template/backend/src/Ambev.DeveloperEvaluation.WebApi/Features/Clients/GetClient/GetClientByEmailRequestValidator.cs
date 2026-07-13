using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;

public class GetClientByEmailRequestValidator : AbstractValidator<GetClientByEmailRequest>
{
    public GetClientByEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required");
    }
}