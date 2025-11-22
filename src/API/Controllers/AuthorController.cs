using Microsoft.AspNetCore.Mvc;
using Domain.IRepositories;
using Domain.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository repository)
    {
        this._authorRepository = repository;
    }
    
    
    [HttpGet]
    public Task<IEnumerable<Author>> GetAllAsync()
    {
        return this._authorRepository.GetAllAsync();
    }
    
}