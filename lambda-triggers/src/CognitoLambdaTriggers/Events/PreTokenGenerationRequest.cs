using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class PreTokenGenerationRequest: RequestBase
{
    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }

    [JsonPropertyName("groupConfiguration")]
    public List<GroupConfiguration> GroupConfiguration { get; }
}
