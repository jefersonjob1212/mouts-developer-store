using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;

public class SearchClientsRequestValidator : AbstractValidator<SearchClientsRequest>
{
    public SearchClientsRequestValidator()
    {
        RuleFor(x => x.Term)
            .NotEmpty()
            .WithMessage("Term is required");
    }
}