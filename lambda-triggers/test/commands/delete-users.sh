#!/bin/bash
set -e

function usage {
    echo ""
    echo "DESCRIPTION:"
    echo "  Deletes all users in Cognito user pool."
    echo ""
    echo "USAGE:"
    echo "  delete-users.sh <COGNITO_USER_POOL_ID>"
}

# make sure user pool ID is given as parameter
if [[ -z "$1" ]]; then
    echo "Please provide a user pool ID"
    usage
    exit 1
fi

# safety check that we're sure we don't delete all users
# in a cognito pool we didn't mean to specify
echo "WARNING: This will delete ALL users in Cognito User Pool: $1"
echo "ARE YOU SURE?"
read -p "Type YES to continue: " response

# check user response
if [[ "${response,,}" != "yes" ]]; then
    echo "Operation cancelled..."
    exit 1
fi

# fetch list of users
COGNITO_USERNAMES=$(aws cognito-idp list-users \
    --user-pool-id $1 \
    --query Users[*].Username \
    --output text)

# delete each one administratively
for user in $COGNITO_USERNAMES; do
    echo "Removing user $user"

    aws cognito-idp admin-delete-user \
        --user-pool-id $1 \
        --username $user
done

echo "Finished."
