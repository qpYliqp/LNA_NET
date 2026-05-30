using API.Features.ProductionSteps.Queries.GetAllProductionStep;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Features.ProductionSteps;

[Route("production-step")]
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