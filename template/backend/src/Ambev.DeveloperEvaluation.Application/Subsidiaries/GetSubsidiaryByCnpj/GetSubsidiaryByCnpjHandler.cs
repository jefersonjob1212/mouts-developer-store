using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByCnpj;

/// <summary>
/// Handler for processing GetSubsidiaryByCnpjCommand requests
/// </summary>
public class GetSubsidiaryByCnpjHandler : IRequestHandler<GetSubsidiaryByCnpjCommand, GetSubsidiaryByCnpjResult>
{
    private readonly ISubsidiaryRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetSubsidiaryByCnpjHandler
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public GetSubsidiaryByCnpjHandler(ISubsidiaryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSubsidiaryByCnpjCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<GetSubsidiaryByCnpjResult> Handle(GetSubsidiaryByCnpjCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSubsidiaryByCnpjValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await _repository.GetByCnpjAsync(request.Cnpj, cancellationToken);
        return _mapper.Map<GetSubsidiaryByCnpjResult>(result);
    }
}