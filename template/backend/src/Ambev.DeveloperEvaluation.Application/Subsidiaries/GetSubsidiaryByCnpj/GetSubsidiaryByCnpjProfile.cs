using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByCnpj;

/// <summary>
/// Profile for mapping between Subsidiary entity and GetProductById responses
/// </summary>
public class GetSubsidiaryByCnpjProfile : Profile
{
    public GetSubsidiaryByCnpjProfile()
    {
        CreateMap<Subsidiary, GetSubsidiaryByCnpjResult>();
    }
}