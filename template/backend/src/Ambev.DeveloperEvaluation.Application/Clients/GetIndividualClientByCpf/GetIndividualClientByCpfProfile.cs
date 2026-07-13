using Ambev.DeveloperEvaluation.Domain;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByCpf;

/// <summary>
/// Profile for mapping between CompanyClient and GetIndividualClientByCpf responses
/// </summary>
public class GetIndividualClientByCpfProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetIndividualClientByCpf operation
    /// </summary>
    public GetIndividualClientByCpfProfile()
    {
        CreateMap<IndividualClient, GetIndividualClientByCpfResult>();
    }
}