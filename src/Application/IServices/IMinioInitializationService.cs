namespace Application.IServices;

public interface IMinioInitializationService
{
    public Task EnsureBucketsExistAsync();
}