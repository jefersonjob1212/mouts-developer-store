using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.SearchClients;

/// <summary>
/// Profile for mapping between CompanyClient and GetCompanyClientsByTradeName responses
/// </summary>
public class SearchClientsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for SearchClients operation
    /// </summary>
    public SearchClientsProfile()
    {
        CreateMap<Client, SearchClientsResult>()
            .Include<IndividualClient, SearchClientsResult>()
            .Include<CompanyClient, SearchClientsResult>();

        CreateMap<IndividualClient, SearchClientsResult>();

        CreateMap<CompanyClient, SearchClientsResult>();
    }
}