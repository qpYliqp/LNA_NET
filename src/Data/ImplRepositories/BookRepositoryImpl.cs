using Microsoft.EntityFrameworkCore;

namespace Data.ImplRepositories;
using Domain.IRepositories;
using Domain.Entities;
public class BookRepositoryImpl : IBookRepository
{
    
    private readonly AppDbContext _dbContext;

    public BookRepositoryImpl(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _dbContext.Books.FindAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }
}