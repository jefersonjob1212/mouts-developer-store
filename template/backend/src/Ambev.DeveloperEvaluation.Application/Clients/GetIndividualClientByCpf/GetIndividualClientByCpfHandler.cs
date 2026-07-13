using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByCpf;

/// <summary>
/// Handler for processing GetIndividualClientByCpfCommand requests
/// </summary>
public class GetIndividualClientByCpfHandler : IRequestHandler<GetIndividualClientByCpfCommand, GetIndividualClientByCpfResult>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetIndividualClientByCpfHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public GetIndividualClientByCpfHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetIndividualClientByCpfCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<GetIndividualClientByCpfResult> Handle(GetIndividualClientByCpfCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetIndividualClientByCpfValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.GetByCpfAsync(request.Cpf, cancellationToken);
        return _mapper.Map<GetIndividualClientByCpfResult>(result);
    }
}