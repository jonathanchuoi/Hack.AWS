using System.ComponentModel.DataAnnotations;

namespace SNS.Infrastructure;

public class BasicAwsCredentialOptions 
{
    public const string SectionName = "AWS";

    [Required]
    public string AccessKeyId { get; set; }
    [Required]
    public string SecretAccessKey { get; set; }
    [Required]
    public string ServiceUrl { get; set; }
}