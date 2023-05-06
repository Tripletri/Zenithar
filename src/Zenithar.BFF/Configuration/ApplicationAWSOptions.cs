namespace Zenithar.BFF.Configuration;

internal sealed class ApplicationAWSOptions
{
    public required string AccessKeyId { get; set; }

    public required string AccessKeySecret { get; set; }
    
    public required string BucketName { get; set; }
};