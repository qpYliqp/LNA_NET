using Domain.Models;
using MediatR;

namespace API.Features.Books.Queries.GetAllBookLetter;

public record GetAllBookLetterByLetterQuery() : IRequest<IDictionary<string, List<BookPreview>>>;
