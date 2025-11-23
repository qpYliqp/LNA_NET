using Application.DTOs.BookDTO;
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
    
    [HttpGet]
    public async Task<IActionResult> GetAllBookWithAuthorAsync()
    {
        var query = new GetAllBookWithAuthorQuery(); 
        var result = await _mediator.Send(query);
        return Ok(result);    }
    
}