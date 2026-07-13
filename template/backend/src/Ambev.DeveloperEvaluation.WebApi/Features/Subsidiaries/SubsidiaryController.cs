using Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryById;
using Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByLegalName;
using Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByTradeName;
using Ambev.DeveloperEvaluation.Application.Subsidiaries.SearchSubsidiaries;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries.GetSubsidiary;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries;

/// <summary>
/// Controllers for managing subsidiaries operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SubsidiaryController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of SubsidiaryController
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    public SubsidiaryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Searches clients by term
    /// </summary>
    /// <param name="term"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetSubsidiaryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchClients([FromQuery] string term, CancellationToken cancellationToken)
    {
        var request = new SearchSubsidiariesRequest { Term = term };

        var validator = new SearchSubsidiariesRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<SearchSubsidiariesCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<GetSubsidiaryResponse>>(response));
    }

    /// <summary>
    /// Retrieve a subsidiary with an ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSubsidiaryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSubsidiaryByIdRequest{ Id = id };
        var validator = new GetSubsidiaryByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetSubsidiaryByIdCommand>(request);
        var result = await _mediator.Send(query, cancellationToken);
        if (result is null)
            return NotFound();
        return Ok(_mapper.Map<GetSubsidiaryResponse>(result));
    }

    /// <summary>
    /// Retrieve a subsidiary with trade name
    /// </summary>
    /// <param name="tradeName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpGet]
    [Route("by-trade-name/{tradeName}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSubsidiaryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTradeName(string tradeName, CancellationToken cancellationToken)
    {
        var request = new GetSubsidiaryByTradeNameRequest{ TradeName = tradeName };
        var validator = new GetSubsidiaryByTradeNameRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetSubsidiaryByTradeNameCommand>(request);
        var result = await _mediator.Send(query, cancellationToken);
        if (!result.Any())
            return NotFound();
        return Ok(_mapper.Map<IEnumerable<GetSubsidiaryResponse>>(result));
    }

    /// <summary>
    /// Retrieve a subsidiary with legal name
    /// </summary>
    /// <param name="legalName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpGet]
    [Route("by-legal-name/{legalName}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSubsidiaryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByLegalName(string legalName, CancellationToken cancellationToken)
    {
        var request = new GetSubsidiaryByLegalNameRequest{ LegalName = legalName };
        var validator = new GetSubsidiaryByLegalNameRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetSubsidiaryByLegalNameCommand>(request);
        var result = await _mediator.Send(query, cancellationToken);
        if (!result.Any())
            return NotFound();
        return Ok(_mapper.Map<IEnumerable<GetSubsidiaryResponse>>(result));
    }
}