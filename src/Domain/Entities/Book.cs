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
}