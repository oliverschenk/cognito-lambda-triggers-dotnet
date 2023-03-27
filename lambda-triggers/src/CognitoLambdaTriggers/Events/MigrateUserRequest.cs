using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class MigrateUserRequest : RequestBase
{
    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }

    [JsonPropertyName("password")]
    public string Password { get; }

    [JsonPropertyName("validationData")]
    public Dictionary<string, string> ValidationData { get; }
}
