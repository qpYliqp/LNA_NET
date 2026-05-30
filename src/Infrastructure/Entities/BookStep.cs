namespace Infrastructure.Entities;

public class BookStep
{
    public int Id { get; set; }
    public DateTime EndDate { get; set; }

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int ProductionStepId { get; set; }
    public ProductionStep ProductionStep { get; set; } = null!;
}
