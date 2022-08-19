
namespace CognitoLambdaTriggers.Core;

public interface ICognitoTriggerEvent
{
    string Version { get; }

    string TriggerSource { get; }

    string Region { get; }

    string UserPoolId { get; }
    
    Guid UserName { get; }

    CallerContext CallerContext { get; }
}
