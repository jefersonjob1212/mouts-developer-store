using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.SearchClients;

/// <summary>
/// Handler for processing SearchClientsCommand requests
/// </summary>
public class SearchClientsHandler : IRequestHandler<SearchClientsCommand, IEnumerable<SearchClientsResult>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of SearchClientsHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public SearchClientsHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the SearchClientsCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<SearchClientsResult>> Handle(SearchClientsCommand request, CancellationToken cancellationToken)
    {
        var validator = new SearchClientsValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.SearchByTermAsync(request.Term, cancellationToken);
        return _mapper.Map<IEnumerable<SearchClientsResult>>(result);
    }
}