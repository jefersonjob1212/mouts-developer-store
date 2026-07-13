using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByLegalName;

/// <summary>
/// Handler for processing GetSubsidiaryByLegalName requests
/// </summary>
public class GetSubsidiaryByLegalNameHandler : IRequestHandler<GetSubsidiaryByLegalNameCommand, IEnumerable<GetSubsidiaryByLegalNameResult>>
{
    private readonly ISubsidiaryRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetSubsidiaryByLegalNameHandler
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public GetSubsidiaryByLegalNameHandler(ISubsidiaryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSubsidiaryByLegalNameCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<IEnumerable<GetSubsidiaryByLegalNameResult>> Handle(GetSubsidiaryByLegalNameCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSubsidiaryByLegalNameValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await _repository.GetByLegalNameAsync(request.LegalName, cancellationToken);
        return _mapper.Map<IEnumerable<GetSubsidiaryByLegalNameResult>>(result);
    }
}