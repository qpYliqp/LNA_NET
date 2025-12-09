using Application.DTOs.BookDTO;
using Application.Features.Books.CreateBookWithAuthor;
using Application.Features.Books.GetAllBook;
using Application.Features.Books.GetAllBookLetter;
using Application.Features.Books.GetAllBookPreview;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;    
    
    [HttpGet]
    public async Task<IActionResult> GetAllBookAsync()
    {
        var query = new GetAllBookQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
    
    
    [HttpGet("preview")]
    public async Task<IActionResult> GetAllBookPreviewAsync()
    {
        var query = new GetAllBookPreviewQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBookWithAuthorAsync([FromBody] CreateBookRequestDto request)
    {
        var query = new CreateBookQuery(request); 
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("letter")]
    public async Task<IActionResult> GetAllBookLetterByLetter()
    {
        var query = new GetAllBookLetterByLetterQuery();
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
}