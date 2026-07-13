using Ambev.DeveloperEvaluation.Domain;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByTradeName;

/// <summary>
/// Profile for mapping between CompanyClient and GetCompanyClientsByTradeName responses
/// </summary>
public class GetCompanyClientsByTradeNameProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCompanyClientsByTradeName operation
    /// </summary>
    public GetCompanyClientsByTradeNameProfile()
    {
        CreateMap<CompanyClient, GetCompanyClientsByTradeNameResult>();
    }
}