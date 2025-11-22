using Application.DTOs.BookDTO;
using Application.IServices;
using Domain.Entities;
using Data.ImplRepositories;
using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{

    private readonly IBookService _bookService;
    
    public BookController(IBookService bookService)
    {
        this._bookService = bookService;
    }

    // public BookController(IBookRepository repository)
    // {
    //     this._bookRepository = repository;
    // }
    //
    //
    // [HttpGet]
    // public Task<IEnumerable<Book>> GetAllAsync()
    // {
    //     return this._bookRepository.GetAllAsync();
    // }
    
    [HttpGet]
    public Task<IReadOnlyList<BookWithAuthorDto>> GetAllBookWithAuthorAsync()
    {
        return this._bookService.GetAllBookWithAuthor();
    }
    
}