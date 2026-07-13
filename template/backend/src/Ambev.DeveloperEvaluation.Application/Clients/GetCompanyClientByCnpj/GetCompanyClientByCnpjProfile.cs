using Ambev.DeveloperEvaluation.Domain;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientByCnpj;

/// <summary>
/// Profile for mapping between CompanyClient and GetCompanyClientByCnpj responses
/// </summary>
public class GetCompanyClientByCnpjProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCompanyClientByCnpj operation
    /// </summary>
    public GetCompanyClientByCnpjProfile()
    {
        CreateMap<CompanyClient, GetCompanyClientByCnpjResult>();
    }
}