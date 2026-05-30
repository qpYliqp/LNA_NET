using Domain.Models;
using MediatR;

namespace API.Features.Books.Commands.UploadBookCover;

/// <summary>
/// Téléverse la couverture d'un livre dans MinIO et met à jour son CoverFileName.
/// </summary>
public record UploadBookCoverCommand(
    int BookId,
    Stream Content,
    string FileName,
    string ContentType) : IRequest<Book>;
