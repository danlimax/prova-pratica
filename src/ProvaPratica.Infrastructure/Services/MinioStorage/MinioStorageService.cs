using Amazon.S3;
using Amazon.S3.Util;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using ProvaPratica.Domain.Interfaces;
using ProvaPratica.Infrastructure.Settings;

namespace ProvaPratica.Infrastructure.Services;

public class MinioStorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly StorageSettings _settings;

    public MinioStorageService(IAmazonS3 s3Client, IOptions<StorageSettings> settings)
    {
        _s3Client = s3Client;
        _settings = settings.Value;
    }

    public async Task EnsureBucketExistsAsync(CancellationToken cancellationToken = default)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _settings.BucketName);
        if (!bucketExists)
            await _s3Client.PutBucketAsync(_settings.BucketName, cancellationToken);
    }
    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

        var request = new PutObjectRequest
        {
            BucketName = _settings.BucketName,
            Key = uniqueFileName,
            InputStream = fileStream,
            ContentType = contentType,
            
        };

        await _s3Client.PutObjectAsync(request, cancellationToken);

        return $"{_settings.PublicUrl}/{_settings.BucketName}/{uniqueFileName}";
    }

    public async Task DeleteAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _settings.BucketName,
            Key = fileName
        };

        await _s3Client.DeleteObjectAsync(request, cancellationToken);
    }
}