using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.SearchSubsidiaries;

/// <summary>
/// Profile for mapping between CompanyClient and GetCompanyClientsByTradeName responses
/// </summary>
public class SearchSubsidiariesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for SearchSubsidiaries operation
    /// </summary>
    public SearchSubsidiariesProfile()
    {
        CreateMap<Subsidiary, SearchSubsidiariesResult>();
    }
}