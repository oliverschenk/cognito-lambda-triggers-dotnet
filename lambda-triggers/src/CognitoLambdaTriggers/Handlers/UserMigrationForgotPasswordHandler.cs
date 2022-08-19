using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class UserMigrationForgotPasswordHandler : CognitoTriggerHandler<MigrateUserEvent>
{
    public const string TRIGGER_SOURCE = "UserMigration_ForgotPassword";

    public override string TriggerSource => TRIGGER_SOURCE;

    public UserMigrationForgotPasswordHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}