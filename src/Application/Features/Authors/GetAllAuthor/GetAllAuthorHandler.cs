using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.Features.Books.GetAllBook;
using Application.Mappers;
using Data;
using Domain.Entities;
using Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Myth.Interfaces;
using Myth.Specifications;

namespace Application.Features.Authors.GetAllAuthor;

public class GetAllAuthorHandler(AppDbContext dbContext) : IRequestHandler<GetAllAuthorQuery, IReadOnlyList<AuthorDto>>
{
    
     private readonly AppDbContext _dbContext = dbContext;

     public async Task<IReadOnlyList<AuthorDto>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
     {
         return await _dbContext.Authors
             .AsNoTracking()
             .Select(a => a.ToDto())
             .ToListAsync(cancellationToken);
     }
    
}