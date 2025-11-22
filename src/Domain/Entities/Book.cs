namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public String Title { get; set; }
    
    public ICollection<Author> Authors { get; set; } = new List<Author>();
}