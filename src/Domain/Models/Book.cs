namespace Domain.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string? Nuart { get; set; }
    public int? Pages { get; set; }
    public float? Price { get; set; }
    public int GlobalStatusId { get; set; } = (int)ProductionStatus.Todo;
    public string? Note { get; set; }
    public string? Summary { get; set; }
    public string? Hook { get; set; }
    public string? Marketing { get; set; }
    public string? CoverFileName { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public ICollection<BookStep> BookSteps { get; set; } = new List<BookStep>();

    /// <summary>
    /// Ajoute une étape de production en validant qu'elle ne dépasse pas la date de sortie.
    /// </summary>
    public void AddBookStep(BookStep step)
    {
        if (ReleaseDate is not null && step.EndDate > ReleaseDate)
        {
            throw new BookDomainException(
                "Une étape de production ne peut pas dépasser la date de sortie.");
        }

        BookSteps.Add(step);
    }

    /// <summary>
    /// Recalcule le statut global du livre à partir de l'état de ses étapes.
    /// </summary>
    public void UpdateGlobalStatus()
    {
        GlobalStatusId = (int)(BookSteps switch
        {
            null or { Count: 0 } => ProductionStatus.Todo,
            var steps when steps.Any(s => s.StatusId == (int)ProductionStatus.Late) => ProductionStatus.Late,
            var steps when steps.Any(s => s.StatusId == (int)ProductionStatus.InProgress) => ProductionStatus.InProgress,
            var steps when steps.All(s => s.StatusId == (int)ProductionStatus.Done) => ProductionStatus.Done,
            _ => ProductionStatus.Todo
        });
    }
}
