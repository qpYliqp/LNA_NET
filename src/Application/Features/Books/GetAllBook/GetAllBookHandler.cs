using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Data;
using Domain.Entities;
using Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Myth.Interfaces;
using Myth.Specifications;

namespace Application.Features.Books.GetAllBook;

public class GetAllBookHandler(AppDbContext dbContext) : IRequestHandler<GetAllBookQuery, IReadOnlyList<BookDto>>
{
    
 private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<BookDto>> Handle(GetAllBookQuery request,
        CancellationToken cancellationToken)
    {
        var spec = SpecBuilder<Book>.Create().StartsWith(request.prefix);
        return await _dbContext.Books
            //permet de spécifier qu'on ne va pas modifier les données mais seulement les lire (opti)
            .AsNoTracking()
            .Where(spec.Predicate)
            .Select(book => new BookDto(
                book.Id,
                book.Title,
                book.Authors.Select(author => new AuthorDto(
                    author.Id,
                    author.Name
                )).ToList()
            ))
            //permet de ne charger les données du livre qu'une seule fois si il y a plusieurs auteurs
            .AsSplitQuery()
            .ToListAsync();
    }
    
}