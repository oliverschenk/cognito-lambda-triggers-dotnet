using Amazon.DynamoDBv2.DataModel;
using Ardalis.GuardClauses;

namespace CognitoLambdaTriggers.Models;

[DynamoDBTable("Users")]
public class User
{
    [DynamoDBHashKey]
    public string UserId { get; private set; } = string.Empty;

    [DynamoDBProperty]
    public string Email { get; private set; } = string.Empty;

    [DynamoDBProperty]
    public string Phone { get; private set; } = string.Empty;
    
    // for DynamdoDbContext
    public User() : base()
    { }

    public User(string userId, string email, string phone) : this()
    {
        Guard.Against.NullOrEmpty(userId);
        Guard.Against.NullOrEmpty(email);
        Guard.Against.NullOrEmpty(phone);

        UserId = userId;
        Email = email;
        Phone = phone;
    }
}
