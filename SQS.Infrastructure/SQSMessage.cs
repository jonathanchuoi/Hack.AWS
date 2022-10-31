using System.Text.Json.Serialization;

namespace SQS.Infrastructure;

public class SQSMessage
{
    [JsonPropertyName("Type")]
    public string Type { get; set; }

    [JsonPropertyName("MessageId")]
    public string MessageId { get; set; }

    [JsonPropertyName("TopicArn")]
    public string TopicArn { get; set; }

    [JsonPropertyName("Message")]
    public string Message { get; set; }

    [JsonPropertyName("Timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("SignatureVersion")]
    public string SignatureVersion { get; set; }

    [JsonPropertyName("Signature")]
    public string Signature { get; set; }

    [JsonPropertyName("SigningCertURL")]
    public string SigningCertURL { get; set; }

    [JsonPropertyName("UnsubscribeURL")]
    public string UnsubscribeURL { get; set; }
}