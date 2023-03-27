#!/bin/bash
set -e

###############################################################################
# This script creates a new random user and signs them up using the Cognito
# SignUp API call.
# 
# Note that the email address will automatically be set to a random alias of
# the BASE_EMAIL_ADDRESS configuration to make testing easier. The phone number
# is generated automatically, but will not be used for verification.
###############################################################################

function usage {
    echo ""
    echo "DESCRIPTION:"
    echo "  Creates a random user in a Cognito User Pool using SignUp API."
    echo "  The base email address will be converted to a random alias email address."
    echo "  For example: user@companyx.com => user+j3k2h4@companyx.com"
    echo ""
    echo "USAGE:"
    echo "  create-user-signup.sh <COGNITO_USER_POOL_CLIENT_ID> <BASE_EMAIL_ADDRESS>"
}

# generate a random number with length as parameter of function
function random_number () {
    digits=$1
    a=$(date +%s)
    b=$((a * RANDOM))

    while [ ${#b} -lt $digits ]; do
        b="${b}$RANDOM"
    done

    echo ${b:0:digits}
}

# make sure user pool client ID is given as parameter
if [[ -z "$1" ]]; then
    echo "Please provide a user pool client ID"
    usage
    exit 1
fi

# make sure email address is given as a parameter
if [[ -z "$2" ]]; then
    echo "Please provide a user registration email address"
    usage
    exit 1
fi

# generate random user details

# create an email alias from the base email address
# e.g. user@address.com -> user+5n3jk3@address.com
BASE_EMAIL_ADDRESS=$2
EMAIL="${BASE_EMAIL_ADDRESS%@*}+$(echo $RANDOM | md5sum | head -c 6)@${BASE_EMAIL_ADDRESS#*@}"
# random phone number
PHONE_NUMBER="+614$(random_number 8)"
# random name
NAME="Susan$RANDOM"

# add a few extra uppercase and special characters to password
# otherwise Cognito will complain about password policy
PASSWORD="$(echo $RANDOM | md5sum | head -c 20)ABC!"

echo "Creating a user with the following details:"
echo "Email=$EMAIL"
echo "Phone=$PHONE_NUMBER"
echo "Name=$NAME"
echo "Password=$PASSWORD"
echo ""

# sign up a user
aws cognito-idp sign-up \
    --client-id $1 \
    --username $EMAIL \
    --password $PASSWORD \
    --user-attributes Name="name",Value="${NAME}" Name="phone_number",Value="$PHONE_NUMBER"

echo "Finished."