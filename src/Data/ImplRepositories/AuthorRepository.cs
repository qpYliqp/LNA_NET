using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.ImplRepositories;
using Domain.Entities;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    
}