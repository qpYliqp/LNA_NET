using API.Features.Authors.Queries.GetAllAuthor;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Features.Authors;

[Route("authors")]
[ApiController]
public class AuthorController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;    

    /// <summary>
    /// Récupère la liste complète des auteurs.
    /// </summary>
    /// <response code="200">Liste récupérée avec succès.</response>
    [HttpGet()]
    public async Task<IActionResult> GetAllAuthorAsync()
    {
        var query = new GetAllAuthorQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
}