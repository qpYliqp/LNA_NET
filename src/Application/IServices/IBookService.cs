using Application.DTOs.BookDTO;
using Domain.Entities;

namespace Application.IServices;

public interface IBookService
{
    
    public Task<IReadOnlyList<BookWithAuthorDto>> GetAllBookWithAuthor();
    
}