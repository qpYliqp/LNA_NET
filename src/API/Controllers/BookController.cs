using Application.DTOs.BookDTO;
using Application.Features.Books.CreateBookWithAuthor;
using Application.Features.Books.GetAllBookWithAuthor;
using Application.IServices;
using Domain.Entities;
using Data.ImplRepositories;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;    
    
    [HttpGet("getAllBookWithAuthor")]
    public async Task<IActionResult> GetAllBookWithAuthorAsync()
    {
        var query = new GetAllBookWithAuthorQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBookWithAuthorAsync([FromBody] CreateBookRequestDto request)
    {
        var query = new CreateBookWithAuthorQuery(request); 
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