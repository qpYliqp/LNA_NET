using Domain.Entities;
using Data.ImplRepositories;
using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{

    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository repository)
    {
        this._bookRepository = repository;
    }
    
    
    [HttpGet]
    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return this._bookRepository.GetAllAsync();
    }
    
}