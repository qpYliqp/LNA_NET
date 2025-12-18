using Application.Features.Status;
using Application.Features.Status.GetAllStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController(IMediator mediator) : ControllerBase
{
    private IMediator _mediator = mediator;
    
    [HttpGet()]
    public async Task<IActionResult> GetAllProductionStepAsync()
    {
        var query = new GetAllStatusQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
}