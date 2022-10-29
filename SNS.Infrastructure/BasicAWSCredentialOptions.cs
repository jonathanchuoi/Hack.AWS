namespace SNS.Infrastructure;

public class StreamOptions
{
    public const string SectionName = "AwsStream";

    // AWS region in which the stream is located
    public string Region { get; set; }
    public string AccountId { get; set; }
    public string Environment { get; set; }
    public string AccessKeyId { get; set; }
    public string SecretAccessKey { get; set; }
    public string Url { get; set; }
}