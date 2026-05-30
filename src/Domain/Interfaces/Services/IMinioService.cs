using Domain.Enumerations;

namespace Domain.Interfaces.Services;

public interface IMinioService
{
    Task<string?> GetFileUrlByNameAsync(string fileName, BucketNames bucketName);

    /// <summary>
    /// Téléverse un fichier dans le bucket indiqué et retourne la clé (nom) de l'objet stocké.
    /// </summary>
    Task<string> UploadFileAsync(Stream content, string fileName, string contentType, BucketNames bucketName);
}
