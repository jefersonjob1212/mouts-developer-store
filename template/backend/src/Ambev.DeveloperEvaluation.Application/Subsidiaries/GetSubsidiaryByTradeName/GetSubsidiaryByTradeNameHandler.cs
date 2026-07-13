using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByTradeName;

/// <summary>
/// Handler for processing GetSubsidiaryByTradeNameCommand requests
/// </summary>
public class GetSubsidiaryByTradeNameHandler : IRequestHandler<GetSubsidiaryByTradeNameCommand, IEnumerable<GetSubsidiaryByTradeNameResult>>
{
    private readonly ISubsidiaryRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetSubsidiaryByTradeNameHandler
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public GetSubsidiaryByTradeNameHandler(ISubsidiaryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSubsidiaryByTradeNameCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<IEnumerable<GetSubsidiaryByTradeNameResult>> Handle(GetSubsidiaryByTradeNameCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSubsidiaryByTradeNameValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await _repository.GetByTradeNameAsync(request.TradeName, cancellationToken);
        return _mapper.Map<IEnumerable<GetSubsidiaryByTradeNameResult>>(result);
    }
}