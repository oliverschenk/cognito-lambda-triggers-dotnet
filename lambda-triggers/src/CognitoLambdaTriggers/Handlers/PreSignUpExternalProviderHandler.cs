using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

namespace CognitoLambdaTriggers.Handlers;

internal class PreSignUpExternalProviderHandler : CognitoTriggerHandler<PreSignUpEvent>
{
    public const string TRIGGER_SOURCE = "PreSignUp_ExternalProvider";

    public override string TriggerSource => TRIGGER_SOURCE;

    private AmazonCognitoIdentityProviderClient cognitoClient;

    public PreSignUpExternalProviderHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
        cognitoClient = new AmazonCognitoIdentityProviderClient();
    }

    public override async Task<JsonElement> HandleTriggerEventAsync()
    {
        var user = await GetUserPoolUserByEmailAsync(
            TriggerEvent.UserPoolId,
            TriggerEvent.Request.UserAttributes["email"]
        );

        if (user != null)
        {
            // event UserName example: "Facebook_12324325436"
            var usernameParts = TriggerEvent.UserName.Split("_");
            var providerName = usernameParts[0];
            var providerUserId = usernameParts[1];

            await LinkProviderForUserAsync(user.Username, TriggerEvent.UserPoolId, providerName, providerUserId);
        }

        return await base.HandleTriggerEventAsync();
    }

    private async Task<UserType> GetUserPoolUserByEmailAsync(string userPoolId, string email)
    {
        var listUsersRequest = new ListUsersRequest()
        {
            UserPoolId = userPoolId,
            Filter = $"email = \"{email}\""
        };

        var listUsersResponse = await cognitoClient.ListUsersAsync(listUsersRequest);

        if (listUsersResponse != null && listUsersResponse.Users.Count > 0)
        {
            return listUsersResponse.Users[0];
        }

        return null;
    }

    private async Task LinkProviderForUserAsync(string username, string userPoolId, 
        string providerName, string providerUserId)
    {
        var request = new AdminLinkProviderForUserRequest()
        {
            SourceUser = {
                ProviderAttributeName = "Cognito_Subject",
                ProviderAttributeValue = providerUserId,
                ProviderName = providerName
            },
            DestinationUser = {
                ProviderName = "Cognito",
                ProviderAttributeValue = username
            },
            UserPoolId = userPoolId,
        };
        
        await cognitoClient.AdminLinkProviderForUserAsync(request);
    }
}