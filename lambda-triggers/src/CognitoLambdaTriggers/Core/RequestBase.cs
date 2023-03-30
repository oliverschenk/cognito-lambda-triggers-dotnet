using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Core;

public abstract class RequestBase
{
    [JsonPropertyName("userAttributes")]
    public Dictionary<string, string> UserAttributes { get; set; }
}