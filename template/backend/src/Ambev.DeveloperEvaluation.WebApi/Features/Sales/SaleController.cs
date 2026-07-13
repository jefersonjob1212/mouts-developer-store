using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SaleController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SaleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Endpoint for retrieves sales by param with paginated
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaginatedAsync([FromQuery] GetSalePaginatedByParamRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new GetSalePaginatedByParamRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<GetSalePaginatedByParamCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<GetSalePaginatedResponse>(result);
        return OkPaginated(
            mapped.SaleList.ToList(), request.PageIndex, result.TotalPages, result.TotalCount);
    }
    
    /// <summary>
    /// Endpoint for retrieves a sale by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleByIdRequest { Id = id };
        var validator = new GetSaleByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<GetSaleByIdCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<GetSaleResponse>(result));
    }

    /// <summary>
    /// Endpoint for create a new sale
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAync([FromBody] CreateSaleRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
    
    /// <summary>
    /// Endpoint for update a sale by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateSaleRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Endpoint for cancel a sale by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch]
    [Route("cancel/{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSaleAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var request = new CancelSaleRequest { Id = id };
        var validator = new CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var command = _mapper.Map<CancelSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<CancelSaleResponse>(response));
    }

    /// <summary>
    /// Endpoint for cancel a sale item by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch]
    [Route("cancel/item/{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSaleItemAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var request = new CancelSaleItemRequest { Id = id };
        var validator = new CancelSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var command = _mapper.Map<CancelSaleItemCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<CancelSaleItemResponse>(response));
    }
}