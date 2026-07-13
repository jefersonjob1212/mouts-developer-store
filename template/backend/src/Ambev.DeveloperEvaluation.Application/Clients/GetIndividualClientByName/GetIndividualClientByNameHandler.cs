using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByName;

/// <summary>
/// Handler for processing GetIndividualClientByNameCommand requests
/// </summary>
public class GetIndividualClientByNameHandler : IRequestHandler<GetIndividualClientByNameCommand, IEnumerable<GetIndividualClientByNameResult>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetIndividualClientByNameHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public GetIndividualClientByNameHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetIndividualClientByNameCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<GetIndividualClientByNameResult>> Handle(GetIndividualClientByNameCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetIndividualClientByNameValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.GetByNameAsync(request.Name, cancellationToken);
        return _mapper.Map<IEnumerable<GetIndividualClientByNameResult>>(result);
    }
}