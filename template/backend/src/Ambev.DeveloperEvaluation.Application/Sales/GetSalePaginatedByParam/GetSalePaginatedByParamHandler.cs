using Ambev.DeveloperEvaluation.Domain.Filters;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;

/// <summary>
/// Handler for processing GetSalePaginatedByParam requests
/// </summary>
public class GetSalePaginatedByParamHandler : IRequestHandler<GetSalePaginatedByParamCommand, GetSalePaginatedResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetSalePaginatedByParamHandler
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public GetSalePaginatedByParamHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSalePaginatedByParamCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<GetSalePaginatedResult> Handle(GetSalePaginatedByParamCommand request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<GetSalePaginatedByParamCommand, SaleFilter>(request);
        var sales = await _repository.GetPaginatedAsync(filter, cancellationToken);
        var count = await _repository.CountAsync(filter, cancellationToken);
        var result = new GetSalePaginatedResult
        {
            TotalCount = count,
            TotalPages = count / request.PageSize,
            SaleList = _mapper.Map<IEnumerable<GetSalePaginatedByParamResult>>(sales)
        };
        return result;
    }
}