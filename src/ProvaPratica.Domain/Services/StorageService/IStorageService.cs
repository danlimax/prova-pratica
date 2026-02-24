namespace ProvaPratica.Domain.Interfaces;

public interface IStorageService
{
    
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default);

    
    Task DeleteAsync(string fileName, CancellationToken cancellationToken = default);
}