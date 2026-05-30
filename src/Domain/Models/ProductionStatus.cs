namespace Domain.Models;

/// <summary>
/// Identifiants des statuts de production (table Status).
/// </summary>
public enum ProductionStatus
{
    Todo = 1,
    InProgress = 2,
    Done = 3,
    Late = 4
}
