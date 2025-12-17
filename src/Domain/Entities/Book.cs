namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public float? Price { get; set; }
    public int? Pages { get; set; }
    public string Isbn { get; set; }
    public int GlobalStatusId { get; set; } = 1;
    public string? Nuart { get; set; }
    public string? Note {  get; set; }
    public string? Summary { get; set; }
    public string? Hook { get; set; }
    public string? Marketing { get; set; }
    public ICollection<Author>? Authors { get; set; } = new List<Author>();
    public string? CoverFileName { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public ICollection<BookStep>? BookSteps { get; set; } = new List<BookStep>();
    
    public void AddBookStep(BookStep step)
    {
        if (step.EndDate > this.ReleaseDate)
        {
            throw new Exception("Une étape de production ne peut pas dépasser la date de sortie.");
        }
        BookSteps.Add(step);
    }    
    
    public void updateGlobalStatus()
    {
        GlobalStatusId = BookSteps switch
        {
            null or { Count: 0 } => 1,
            //Si au moins un "En retard"
            var steps when steps.Any(s => s.StatusId == 4) => 4,
            //Si au moins un "En cours"
            var steps when steps.Any(s => s.StatusId == 2) => 2,
            //Si TOUS sont "Terminé"
            var steps when steps.All(s => s.StatusId == 3) => 3,
            //Cas par défaut (ex: mélange de "À faire" et "Terminé")
            _ => 1
        };
    }
}