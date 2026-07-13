using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByTradeName;

/// <summary>
/// Handler for processing GetCompanyClientsByTradeNameCommand requests
/// </summary>
public class GetCompanyClientsByTradeNameHandler : IRequestHandler<GetCompanyClientsByTradeNameCommand, IEnumerable<GetCompanyClientsByTradeNameResult>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetCompanyClientsByTradeNameHandler
    /// </summary>
    /// <param name="clientRepository"></param>
    /// <param name="mapper"></param>
    public GetCompanyClientsByTradeNameHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCompanyClientsByTradeNameCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<GetCompanyClientsByTradeNameResult>> Handle(GetCompanyClientsByTradeNameCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCompanyClientsByTradeNameValidator();
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        var result = await _clientRepository.GetByLegalNameAsync(request.TradeName, cancellationToken);
        return _mapper.Map<IEnumerable<GetCompanyClientsByTradeNameResult>>(result);
    }
}