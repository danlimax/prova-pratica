using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProvaPratica.Domain.Interfaces;
using ProvaPratica.Infrastructure.Services;
using ProvaPratica.Infrastructure.Settings;

namespace ProvaPratica.Infrastructure.Extensions;

public static class StorageExtensions
{
    public static IServiceCollection AddStorageService(this IServiceCollection services, IConfiguration configuration)
    {
      
        services.Configure<StorageSettings>(opts =>
            configuration.GetSection(StorageSettings.SectionName).Bind(opts));

        var settings = configuration.GetSection(StorageSettings.SectionName).Get<StorageSettings>()!;

        var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);

        var config = new AmazonS3Config
        {
            ServiceURL = settings.ServiceUrl,
            ForcePathStyle = settings.ForcePathStyle,
            UseHttp = true                            
        };

        services.AddSingleton<IAmazonS3>(new AmazonS3Client(credentials, config));
        services.AddScoped<IStorageService, MinioStorageService>();

        return services;
    }
}