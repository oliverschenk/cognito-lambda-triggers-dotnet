using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;
using CognitoLambdaTriggers.Models;

namespace CognitoLambdaTriggers.Handlers;

internal class PostConfirmationConfirmSignUpHandler : CognitoTriggerHandler<PostConfirmationEvent>
{
    public const string TRIGGER_SOURCE = "PostConfirmation_ConfirmSignUp";

    public override string TriggerSource => TRIGGER_SOURCE;

    private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();

    public PostConfirmationConfirmSignUpHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public async override Task<JsonElement> HandleTriggerEventAsync()
    {
        var userId = TriggerEvent.UserName;
        var userEmail = TriggerEvent.Request.UserAttributes["email"];
        var userPhone = TriggerEvent.Request.UserAttributes["phone_number"];
        
        var user = new User(userId, userEmail, userPhone);

        await StoreUser(user);

        return await base.HandleTriggerEventAsync();
    }

    private async Task StoreUser(User user)
    {
        try
        {
            DynamoDBContext context = new DynamoDBContext(client);
            await context.SaveAsync(user);
        }
        catch (AmazonDynamoDBException e) { Logger.LogError(e.Message); }
        catch (AmazonServiceException e) { Logger.LogError(e.Message); }
        catch (Exception e) { Logger.LogError(e.Message); }
    }
}