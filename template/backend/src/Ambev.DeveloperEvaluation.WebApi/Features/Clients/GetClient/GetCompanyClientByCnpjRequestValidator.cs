using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;

public class GetCompanyClientByCnpjRequestValidator : AbstractValidator<GetCompanyClientByCnpjRequest>
{
    public GetCompanyClientByCnpjRequestValidator()
    {
        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("Cnpj is required");
    }
}