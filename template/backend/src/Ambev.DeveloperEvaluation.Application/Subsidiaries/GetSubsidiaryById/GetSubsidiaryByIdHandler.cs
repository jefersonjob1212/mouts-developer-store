using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryById;

/// <summary>
/// Handler for processing GetSubsidiaryById requests
/// </summary>
public class GetSubsidiaryByIdHandler : IRequestHandler<GetSubsidiaryByIdCommand, GetSubsidiaryByIdResult>
{
    private readonly ISubsidiaryRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetSubsidiaryByIdHandler
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public GetSubsidiaryByIdHandler(ISubsidiaryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSubsidiaryByIdCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<GetSubsidiaryByIdResult> Handle(GetSubsidiaryByIdCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSubsidiaryByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<GetSubsidiaryByIdResult>(result);
    }
}