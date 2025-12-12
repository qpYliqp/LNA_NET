namespace Application.Configuration;

public class MinioSettings
{
    public const string SectionName = "MinioSettings"; 
    
    public string Endpoint { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public List<MinioBucketSetting> Buckets { get; set; } = new List<MinioBucketSetting>();
}