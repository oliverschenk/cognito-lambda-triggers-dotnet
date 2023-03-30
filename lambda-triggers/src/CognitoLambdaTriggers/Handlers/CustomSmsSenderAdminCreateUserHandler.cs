using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomSmsSenderAdminCreateUserHandler : CognitoTriggerHandler<CustomSmsSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomSMSSender_AdminCreateUser";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomSmsSenderAdminCreateUserHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}