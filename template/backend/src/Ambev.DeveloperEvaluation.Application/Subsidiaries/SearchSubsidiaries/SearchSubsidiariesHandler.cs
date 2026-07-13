using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.SearchSubsidiaries;

/// <summary>
/// Handler for processing SearchSubsidiariesCommand requests
/// </summary>
public class SearchSubsidiariesHandler : IRequestHandler<SearchSubsidiariesCommand, IEnumerable<SearchSubsidiariesResult>>
{
    private readonly ISubsidiaryRepository _subsidiaryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of SearchSubsidiariesHandler
    /// </summary>
    /// <param name="subsidiaryRepository"></param>
    /// <param name="mapper"></param>
    public SearchSubsidiariesHandler(ISubsidiaryRepository subsidiaryRepository, IMapper mapper)
    {
        _subsidiaryRepository = subsidiaryRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the SearchSubsidiariesCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<SearchSubsidiariesResult>> Handle(SearchSubsidiariesCommand request, CancellationToken cancellationToken)
    {
        var validator = new SearchSubsidiariesValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _subsidiaryRepository.SearchByTermAsync(request.Term, cancellationToken);
        return _mapper.Map<IEnumerable<SearchSubsidiariesResult>>(result);
    }
}