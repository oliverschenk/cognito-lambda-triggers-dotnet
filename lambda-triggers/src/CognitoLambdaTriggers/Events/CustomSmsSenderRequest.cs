using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class CustomSmsSenderRequest: RequestBase
{
    [JsonPropertyName("type")]
    public string Type { get; } = "customSMSSenderRequestV1";

    [JsonPropertyName("code")]
    public string Code { get; }

    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }
}
