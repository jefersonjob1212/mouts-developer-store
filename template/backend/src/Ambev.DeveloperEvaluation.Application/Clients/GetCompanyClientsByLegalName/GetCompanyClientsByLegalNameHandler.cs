using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByLegalName;

/// <summary>
/// Handler for processing GetCompanyClientsByLegalNameCommand requests
/// </summary>
public class GetCompanyClientsByLegalNameHandler : IRequestHandler<GetCompanyClientsByLegalNameCommand, IEnumerable<GetCompanyClientsByLegalNameResult>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetCompanyClientsByLegalNameHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public GetCompanyClientsByLegalNameHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCompanyClientsByLegalNameCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<GetCompanyClientsByLegalNameResult>> Handle(GetCompanyClientsByLegalNameCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCompanyClientsByLegalNameValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.GetByLegalNameAsync(request.LegalName, cancellationToken);
        return _mapper.Map<IEnumerable<GetCompanyClientsByLegalNameResult>>(result);
    }
}