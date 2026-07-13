using Ambev.DeveloperEvaluation.Application.Clients.GetClientByEmail;
using Ambev.DeveloperEvaluation.Application.Clients.GetClientById;
using Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientByCnpj;
using Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByLegalName;
using Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByTradeName;
using Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByCpf;
using Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByName;
using Ambev.DeveloperEvaluation.Application.Clients.SearchClients;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Clients;

/// <summary>
/// Controllers for managing clients operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of ClientController
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    public ClientController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a client by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClientById(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetClientByIdRequest(id);

        var validator = new GetClientByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetClientByIdCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<GetClientResponse>(response));
    }

    /// <summary>
    /// Retrieves a client by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("email/{email}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClientByEmail(string email, CancellationToken cancellationToken)
    {
        var request = new GetClientByEmailRequest { Email = email };

        var validator = new GetClientByEmailRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetClientByEmailCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<GetClientResponse>(response));
    }

    /// <summary>
    /// Retrieves an individual client by CPF
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("individual/cpf/{cpf}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIndividualClientByCpf(string cpf, CancellationToken cancellationToken)
    {
        var request = new GetIndividualClientByCpfRequest { Cpf = cpf };

        var validator = new GetIndividualClientByCpfRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetIndividualClientByCpfCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<GetClientResponse>(response));
    }

    /// <summary>
    /// Retrieves a company client by CNPJ
    /// </summary>
    /// <param name="cnpj"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("company/cnpj/{cnpj}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyClientByCnpj(string cnpj, CancellationToken cancellationToken)
    {
        var request = new GetCompanyClientByCnpjRequest { Cnpj = cnpj };

        var validator = new GetCompanyClientByCnpjRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCompanyClientByCnpjCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<GetClientResponse>(response));
    }

    /// <summary>
    /// Searches clients by term
    /// </summary>
    /// <param name="term"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetClientResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchClients([FromQuery] string term, CancellationToken cancellationToken)
    {
        var request = new SearchClientsRequest { Term = term };

        var validator = new SearchClientsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<SearchClientsCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<GetClientResponse>>(response));
    }
}