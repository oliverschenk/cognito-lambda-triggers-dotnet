using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class PreAuthenticationRequest: RequestBase
{
    [JsonPropertyName("validationData")]
    public Dictionary<string, string> ValidationData { get; }

    [JsonPropertyName("userNotFound")]
    public bool UserNotFound { get; }
}
