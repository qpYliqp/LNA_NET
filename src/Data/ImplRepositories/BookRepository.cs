using Microsoft.EntityFrameworkCore;

namespace Data.ImplRepositories;
using Domain.IRepositories;
using Domain.Entities;
public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    private readonly AppDbContext _dbContext =  dbContext;

}