using Ambev.DeveloperEvaluation.Domain;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientByEmail;

/// <summary>
/// Profile for mapping between IndividualClient entity or CompanyClient and GetClientByEmail responses
/// </summary>
public class GetClientByEmailProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetClientByEmail operation
    /// </summary>
    public GetClientByEmailProfile()
    {
        CreateMap<IndividualClient, GetClientByEmailResult>();
        CreateMap<CompanyClient, GetClientByEmailResult>();
    }
}