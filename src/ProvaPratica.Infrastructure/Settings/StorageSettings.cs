namespace ProvaPratica.Infrastructure.Settings;

public class StorageSettings
{
    public const string SectionName = "Storage";

    public string ServiceUrl { get; set; } = string.Empty;   
    public string PublicUrl { get; set; } = string.Empty;    
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;   
    public bool ForcePathStyle { get; set; } = true;          
}