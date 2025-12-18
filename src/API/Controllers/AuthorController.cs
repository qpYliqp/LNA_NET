using Application.Features.Authors.GetAllAuthor;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;    

    [HttpGet()]
    public async Task<IActionResult> GetAllAuthorAsync()
    {
        var query = new GetAllAuthorQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
}