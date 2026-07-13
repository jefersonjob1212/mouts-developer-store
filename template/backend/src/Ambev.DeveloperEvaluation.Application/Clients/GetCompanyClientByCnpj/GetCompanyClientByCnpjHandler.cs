using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientByCnpj;

/// <summary>
/// Handler for processing GetCompanyClientByCnpjCommand requests
/// </summary>
public class GetCompanyClientByCnpjHandler : IRequestHandler<GetCompanyClientByCnpjCommand, GetCompanyClientByCnpjResult>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetCompanyClientByCnpjHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public GetCompanyClientByCnpjHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCompanyClientByCnpjCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<GetCompanyClientByCnpjResult> Handle(GetCompanyClientByCnpjCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCompanyClientByCnpjValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.GetByCnpjAsync(request.Cnpj, cancellationToken);
        return _mapper.Map<GetCompanyClientByCnpjResult>(result);
    }
}