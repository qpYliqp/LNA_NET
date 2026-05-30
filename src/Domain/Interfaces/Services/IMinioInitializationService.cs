namespace Domain.Interfaces.Services;

public interface IMinioInitializationService
{
    public Task EnsureBucketsExistAsync();
}