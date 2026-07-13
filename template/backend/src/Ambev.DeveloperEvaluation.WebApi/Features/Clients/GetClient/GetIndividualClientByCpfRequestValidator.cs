using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;

public class GetIndividualClientByCpfRequestValidator : AbstractValidator<GetIndividualClientByCpfRequest>
{
    public GetIndividualClientByCpfRequestValidator()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty()
            .WithMessage("Cpf is required");
    }
}