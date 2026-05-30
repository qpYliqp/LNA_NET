namespace Domain.Models;

public class BookStep
{
    public int Id { get; set; }
    public int ProductionStepId { get; set; }
    public int StatusId { get; set; }
    public DateTime EndDate { get; set; }
}
