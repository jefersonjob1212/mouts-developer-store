using Ambev.DeveloperEvaluation.Application.Clients.GetClientByEmail;
using Ambev.DeveloperEvaluation.Application.Clients.GetClientById;
using Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientByCnpj;
using Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByCpf;
using Ambev.DeveloperEvaluation.Application.Clients.SearchClients;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;

public class GetClientProfile : Profile
{
    public GetClientProfile()
    {
        CreateMap<GetClientByEmailRequest, GetClientByEmailCommand>();
        CreateMap<GetClientByIdRequest, GetClientByIdCommand>();
        CreateMap<GetCompanyClientByCnpjRequest, GetCompanyClientByCnpjCommand>();
        CreateMap<GetIndividualClientByCpfRequest, GetIndividualClientByCpfCommand>();
        CreateMap<SearchClientsRequest, SearchClientsCommand>();
        CreateMap<GetClientByEmailResult, GetClientResponse>();
        CreateMap<GetClientByIdResult, GetClientResponse>();
        CreateMap<GetCompanyClientByCnpjResult, GetClientResponse>();
        CreateMap<GetIndividualClientByCpfResult, GetClientResponse>();
        CreateMap<SearchClientsResult, GetClientResponse>();
    }
}