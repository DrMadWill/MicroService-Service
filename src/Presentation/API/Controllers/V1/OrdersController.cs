using System.Net;
using API.Extensions;
using Domain.Entities;
using DrMadWill.Extensions.Response;
using DrMadWill.IdentityService.Abstractions;
using DrMadWill.Layers.Repository.Extensions.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;

public class OrdersController : BaseV1Controller
{
    private readonly IMediator _mediator;
    private readonly IUserTokenManager _userTokenManager;
    public OrdersController(IMediator mediator, IUserTokenManager userTokenManager)
    {
        _mediator = mediator;
        _userTokenManager = userTokenManager;
    }

    // [HttpGet]
    // [ProducesResponseType(typeof(GenericResponse<SourcePaged<OrderDto>>),(int)HttpStatusCode.OK)]
    // [ProducesResponseType(typeof( GenericResponse<string>),(int)HttpStatusCode.BadRequest)]
    // public async Task<IActionResult> GetAll([FromQuery]GetAllOrderReq req )
    // {
    //     req = WriteUerInfo(req);
    //     var result = await _mediator.Send(WriteLang(req));
    //     return Ok(SysResponse.Succeed(result));
    // }
    //
    // [HttpGet("{id:guid}")]
    // [ProducesResponseType(typeof(GenericResponse<OrderDto>),(int)HttpStatusCode.OK)]
    // public async Task<IActionResult> Get(Guid id)
    // {
    //     var result = await _mediator.Send( WriteUerInfo(WriteLang(new GetOrderReq { Id = id })));
    //     return Ok(SysResponse.Succeed((result)));
    // } 
    //
    //
    
    

    
}