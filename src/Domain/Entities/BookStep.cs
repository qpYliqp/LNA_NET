namespace Domain.Entities;

public class BookStep
{
    public int Id { get; set; }
    public DateTime EndDate { get; set; }
    
    public Status Status { get; set; }
    public int StatusId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
    
    public ProductionStep ProductionStep { get; set; }
    public int ProductionStepId { get; set; }
}