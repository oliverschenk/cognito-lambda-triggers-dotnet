using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class CustomMessageRequest: RequestBase
{
    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }

    [JsonPropertyName("codeParameter")]
    public string CodeParameter { get; }

    [JsonPropertyName("usernameParameter")]
    public string UsernameParameter { get; }
}
