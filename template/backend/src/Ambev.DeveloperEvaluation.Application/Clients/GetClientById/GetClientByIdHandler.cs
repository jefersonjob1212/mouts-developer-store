using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientById;

/// <summary>
/// Handler for processing GetClientByIdCommand requests
/// </summary>
public class GetClientByIdHandler : IRequestHandler<GetClientByIdCommand, GetClientByIdResult>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetClientByIdHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public GetClientByIdHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetClientByIdCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<GetClientByIdResult> Handle(GetClientByIdCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetClientByIdValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);
        return result switch
        {
            IndividualClient individual => _mapper.Map<GetClientByIdResult>(result),
            CompanyClient company => _mapper.Map<GetClientByIdResult>(result),
            _ => throw new InvalidOperationException()
        };
    }
}