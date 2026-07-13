using Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByCnpj;
using Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryById;
using Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByLegalName;
using Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByTradeName;
using Ambev.DeveloperEvaluation.Application.Subsidiaries.SearchSubsidiaries;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries.GetSubsidiary;

/// <summary>
/// Profile for GetSubsidiary requests and responses
/// </summary>
public class GetSubsidiaryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSubsidiary feature
    /// </summary>
    public GetSubsidiaryProfile()
    {
        CreateMap<GetSubsidiaryByIdRequest, GetSubsidiaryByIdCommand>();
        CreateMap<GetSubsidiaryByCnpjRequest, GetSubsidiaryByCnpjCommand>();
        CreateMap<GetSubsidiaryByLegalNameRequest, GetSubsidiaryByLegalNameCommand>();
        CreateMap<GetSubsidiaryByTradeNameRequest, GetSubsidiaryByTradeNameCommand>();
        CreateMap<SearchSubsidiariesRequest, SearchSubsidiariesCommand>();
        CreateMap<GetSubsidiaryByIdResult, GetSubsidiaryResponse>();
        CreateMap<GetSubsidiaryByCnpjResult, GetSubsidiaryResponse>();
        CreateMap<GetSubsidiaryByLegalNameResult, GetSubsidiaryResponse>();
        CreateMap<GetSubsidiaryByTradeNameResult, GetSubsidiaryResponse>();
        CreateMap<SearchSubsidiariesResult, GetSubsidiaryResponse>();
    }
}