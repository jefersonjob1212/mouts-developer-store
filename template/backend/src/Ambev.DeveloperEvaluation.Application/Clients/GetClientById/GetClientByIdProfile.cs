using Ambev.DeveloperEvaluation.Domain;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientById;

/// <summary>
/// Profile for mapping between IndividualClient entity or CompanyClient and GetClientById responses
/// </summary>
public class GetClientByIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetClientById operation
    /// </summary>
    public GetClientByIdProfile()
    {
        CreateMap<IndividualClient, GetClientByIdResult>();
        CreateMap<CompanyClient, GetClientByIdResult>();
    }
}