using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

/// <summary>
/// Handler for processing GetSaleById requests
/// </summary>
public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdCommand, GetSaleByIdResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetSaleByIdHandler
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public GetSaleByIdHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleByIdCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<GetSaleByIdResult> Handle(GetSaleByIdCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await _repository.GetByIdWithItemsAsync(request.Id, cancellationToken);
        return _mapper.Map<GetSaleByIdResult>(result);
    }
}