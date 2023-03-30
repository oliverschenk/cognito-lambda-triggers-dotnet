using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Core;

public class CognitoTriggerEventBase : ICognitoTriggerEvent 
{
    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("triggerSource")]
    public string TriggerSource { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("userPoolId")]
    public string UserPoolId { get; set; }
    
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("callerContext")]
    public CallerContext CallerContext { get; set; }
}
