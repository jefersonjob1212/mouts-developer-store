using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries.GetSubsidiary;

public class SearchSubsidiariesRequestValidator : AbstractValidator<SearchSubsidiariesRequest>
{
    public SearchSubsidiariesRequestValidator()
    {
        RuleFor(x => x.Term)
            .NotEmpty()
            .WithMessage("Term is required");
    }
}