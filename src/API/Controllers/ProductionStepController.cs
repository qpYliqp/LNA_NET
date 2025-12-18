using Application.Features.ProductionStep;
using Application.Features.ProductionStep.GetAllProductionStep;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductionStepController(IMediator mediator) : ControllerBase
{
    private IMediator _mediator = mediator;
    
    [HttpGet()]
    public async Task<IActionResult> GetAllProductionStepAsync()
    {
        var query = new GetAllProductionStepQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
}