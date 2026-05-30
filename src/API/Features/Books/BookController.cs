using API.Features.Books.Commands.CreateBook;
using API.Features.Books.Commands.UpdateBook;
using API.Features.Books.Commands.UploadBookCover;
using API.Features.Books.Queries.GetAllBook;
using API.Features.Books.Queries.GetAllBookLetter;
using API.Features.Books.Queries.GetAllBookPreview;
using API.Features.Books.Queries.GetBookById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Features.Books;

[Route("books")]
[ApiController]
public class BookController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Récupère la liste complète des livres.
    /// </summary>
    /// <remarks>Retourne tous les livres, ou ceux commençant par un préfixe.</remarks>
    /// <param name="prefix">Préfixe optionnel de filtrage sur le titre.</param>
    /// <response code="200">Liste récupérée avec succès.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllBookAsync([FromQuery] string? prefix)
    {
        var result = await _mediator.Send(new GetAllBookQuery(prefix));
        return Ok(result);
    }

    [HttpGet("preview")]
    public async Task<IActionResult> GetAllBookPreviewAsync()
    {
        var result = await _mediator.Send(new GetAllBookPreviewQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookByIdAsync(int id)
    {
        var result = await _mediator.Send(new GetBookByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("letter")]
    public async Task<IActionResult> GetAllBookLetterByLetter()
    {
        var result = await _mediator.Send(new GetAllBookLetterByLetterQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        var createdBook = await _mediator.Send(new CreateBookCommand(request));
        return Ok(createdBook);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
    {
        var updatedBook = await _mediator.Send(new UpdateBookCommand(request));
        return Ok(updatedBook);
    }

    /// <summary>
    /// Téléverse la couverture d'un livre dans le bucket MinIO "cover".
    /// </summary>
    /// <param name="id">Identifiant du livre.</param>
    /// <param name="file">Fichier image de la couverture (jpg, jpeg, png, webp).</param>
    /// <response code="200">Couverture téléversée et CoverFileName mis à jour.</response>
    /// <response code="400">Fichier manquant ou format non supporté.</response>
    [HttpPost("{id:int}/cover")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadCover(int id, IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("Aucun fichier n'a été fourni.");
        }

        await using var stream = file.OpenReadStream();
        var command = new UploadBookCoverCommand(id, stream, file.FileName, file.ContentType);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
