using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.ImplRepositories;
using Domain.Entities;

public class AuthorRepositoryImpl : IAuthorRepository
{
    private readonly AppDbContext _dbContext;

    public AuthorRepositoryImpl(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        return await _dbContext.Authors.FindAsync(id);
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _dbContext.Authors.ToListAsync();
    }
}