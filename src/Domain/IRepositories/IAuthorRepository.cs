namespace Domain.IRepositories;
using Domain.Entities;
public interface IAuthorRepository 
{
    Task<Author> GetByIdAsync(int id);
    
    Task<IEnumerable<Author>> GetAllAsync();
}