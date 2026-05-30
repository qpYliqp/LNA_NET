namespace Domain.Models;

/// <summary>
/// Exception métier levée lorsqu'une règle de gestion d'un livre est violée.
/// Permet au middleware de renvoyer un statut HTTP 400 plutôt qu'un 500.
/// </summary>
public class BookDomainException : Exception
{
    public BookDomainException(string message) : base(message) { }
}
