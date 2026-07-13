using Ambev.DeveloperEvaluation.Domain;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByName;

/// <summary>
/// Profile for mapping between CompanyClient and GetIndividualClientByName responses
/// </summary>
public class GetIndividualClientByNameProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetIndividualClientByName operation
    /// </summary>
    public GetIndividualClientByNameProfile()
    {
        CreateMap<IndividualClient, GetIndividualClientByNameResult>();
    }
}