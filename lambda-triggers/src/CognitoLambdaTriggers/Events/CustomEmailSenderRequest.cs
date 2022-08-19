using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class CustomEmailSenderRequest: RequestBase
{
    [JsonPropertyName("type")]
    public string Type { get; } = "customEmailSenderRequestV1";

    [JsonPropertyName("code")]
    public string Code { get; }

    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }
}
