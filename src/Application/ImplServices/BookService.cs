using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.IServices;
using Data;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Application.ImplServices;

public class BookService(AppDbContext dbContext) : IBookService{
    
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<BookDto>> GetAllBookWithAuthor()
    {
        return await _dbContext.Books
            .Include(b => b.Authors) 
            .Select(book => new BookDto(
                book.Id,
                book.Title,
                book.Authors.Select(author => new AuthorDto(
                    author.Id,
                    author.Name
                )).ToList() 
            ))
            .ToListAsync();
    }
    
}