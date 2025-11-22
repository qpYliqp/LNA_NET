using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.IServices;
using Domain.IRepositories;

namespace Application.ImplServices;

public class BookService : IBookService
{
    
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IReadOnlyList<BookWithAuthorDto>> GetAllBookWithAuthor()
    {
        var books = await _bookRepository.GetAllAsync();

        return books.Select(book => new BookWithAuthorDto(book.Id,
            book.Title,
            book.Authors.Select(author => new AuthorDto(
                author.Id,
                author.Name
        )).ToList())).ToList();
    }
    
}