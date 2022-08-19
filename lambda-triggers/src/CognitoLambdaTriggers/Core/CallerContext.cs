using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Core;

public class CallerContext
{
    [JsonPropertyName("awsSdkVersion")]
    public string AwsSdkVersion { get; set; }

    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }
}
