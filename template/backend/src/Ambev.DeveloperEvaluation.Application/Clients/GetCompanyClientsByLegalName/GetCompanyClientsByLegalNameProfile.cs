using Ambev.DeveloperEvaluation.Domain;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByLegalName;

/// <summary>
/// Profile for mapping between CompanyClient and GetCompanyClientsByLegalName responses
/// </summary>
public class GetCompanyClientsByLegalNameProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCompanyClientsByLegalName operation
    /// </summary>
    public GetCompanyClientsByLegalNameProfile()
    {
        CreateMap<CompanyClient, GetCompanyClientsByLegalNameResult>();
    }
}