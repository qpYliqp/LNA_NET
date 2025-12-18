using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.Mappers;
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

 public async Task<IReadOnlyList<BookDto>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
 {
     var spec = SpecBuilder<Book>.Create().StartsWith(request.prefix);

     return await _dbContext.Books
        //AsNoTracking --> Indique que c'est en lecture seule
         .AsNoTracking()
         .AsSplitQuery()
         .Where(spec.Predicate)
         .Select(b => b.ToDto())
         .ToListAsync(cancellationToken);
 }
    
}