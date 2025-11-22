namespace Domain.IRepositories;
using Domain.Entities;

public interface IBookRepository
{
    Task<Book> GetByIdAsync(int id);
    
    Task<IEnumerable<Book>> GetAllAsync();
}