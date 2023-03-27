#!/bin/bash
set -e

NAME='cognito-lambda-triggers'

TF_ACTION='apply'

DEFAULT_RELEASE_TYPE='Debug'
DEFAULT_REGION='ap-southeast-2'
DEFAULT_STAGE='dev'

REGION=$DEFAULT_REGION
STAGE=$DEFAULT_STAGE
RELEASE_TYPE=$DEFAULT_RELEASE_TYPE

function usage {
    echo "DESCRIPTION:"
    echo "  Script for deploying resources to AWS."
    echo ""
    echo "USAGE:"
    echo "  deploy.sh [-p profile] [-r region] [-s stage]"
    echo ""
    echo "OPTIONS"
    echo "  -p   the AWS credentials profile to use (default: none)"
    echo "  -r   region (default: ap-southeast-2)"
    echo "  -s   the stage to deploy [dev, test, prod] (default: dev)"
}

function pushd() {
    command pushd "$@" >/dev/null
}

function popd() {
    command popd "$@" >/dev/null
}

while getopts "p:r:s" option; do
    case ${option} in
    p) AWS_PROFILE=$OPTARG ;;
    r) REGION=$OPTARG ;;
    s) STAGE=$OPTARG ;;
    \?)
        echo "Invalid option: -$OPTARG" 1>&2
        usage
        exit 1
        ;;
    esac
done

if [[ -n "${VALIDATION_ERROR}" ]]; then
    usage
    exit 1
fi

if [[ -n "${AWS_PROFILE}" ]]; then
    export AWS_PROFILE=$AWS_PROFILE
    AWS_PROFILE="none"
fi

case ${STAGE,,} in

dev)
    RELEASE_TYPE='Debug'
    ;;

test | prod)
    RELEASE_TYPE='Release'
    ;;

*)
    RELEASE_TYPE='Release'
    ;;
esac

##############################################################

echo "=== Using the following parameters ==="
echo "Region: ${REGION}"
echo "Stage: ${STAGE}"
echo "Profile: ${AWS_PROFILE}"

##############################################################
pushd ./lambda-triggers/src/CognitoLambdaTriggers

echo ""
echo "=== Building Lambda triggers ==="

echo ""
echo "=== Restoring dependencies ==="
dotnet restore

echo ""
echo "=== Building dotnet project ==="
dotnet build -c ${RELEASE_TYPE}

echo ""
echo "=== Creating lambda package ==="
dotnet lambda package -c ${RELEASE_TYPE} -o ../../output/CognitoLambdaTriggers.zip
echo ""

popd
##############################################################
echo ""
echo "=== Applying action: ${TF_ACTION} ==="

pushd ./infrastructure

terraform init
terraform ${TF_ACTION} --var aws_region=${REGION} --var project_name=${NAME}

popd
##############################################################

echo ""
echo "Completed."
