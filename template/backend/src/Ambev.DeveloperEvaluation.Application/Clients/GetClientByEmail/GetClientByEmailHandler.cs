using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientByEmail;

/// <summary>
/// Handler for processing GetClientByEmailCommand requests
/// </summary>
public class GetClientByEmailHandler : IRequestHandler<GetClientByEmailCommand, GetClientByEmailResult>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetClientByEmailHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public GetClientByEmailHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetClientByEmailCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<GetClientByEmailResult> Handle(GetClientByEmailCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetClientByEmailValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.GetByEmailAsync(request.Email, cancellationToken);
        return result switch
        {
            IndividualClient individual => _mapper.Map<GetClientByEmailResult>(result),
            CompanyClient company => _mapper.Map<GetClientByEmailResult>(result),
            _ => throw new InvalidOperationException()
        };
    }
}