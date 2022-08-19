using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class PreTokenGenerationResponse
{
    [JsonPropertyName("autoConfirmUser")]
    public bool AutoConfirmUser { get; set; }
    
    [JsonPropertyName("autoVerifyPhone")]
    public bool AutoVerifyPhone { get; set; }
    
    [JsonPropertyName("autoVerifyEmail")]
    public bool AutoVerifyEmail { get; set; }        
}
