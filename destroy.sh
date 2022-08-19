#!/bin/bash
set -e

NAME='cognito-lambda-triggers'

TF_ACTION='destroy'

DEFAULT_RELEASE_TYPE='Debug'
DEFAULT_REGION='ap-southeast-2'
DEFAULT_STAGE='dev'

AWS_VAULT_PREFIX=''

REGION=$DEFAULT_REGION
STAGE=$DEFAULT_STAGE

function usage {
    echo "DESCRIPTION:"
    echo "  Script for destroying resources on AWS."
    echo ""
    echo "USAGE:"
    echo "  destroying.sh [-p credentials_profile] [-r region] [-s stage]"
    echo ""
    echo "OPTIONS"
    echo "  -p   the credentials profile to use (uses aws-vault)"
    echo "  -r   region (default: ap-southeast-2)"
    echo "  -s   the stage to destroy [dev, test, prod] (default: dev)"
}

function aws_exec {
    ${AWS_VAULT_PREFIX}$1
}

function pushd() {
    command pushd "$@" >/dev/null
}

function popd() {
    command popd "$@" >/dev/null
}

while getopts "p:r:s" option; do
    case ${option} in
    p) AWS_VAULT_PROFILE=$OPTARG ;;
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

if [[ -n "${AWS_VAULT_PROFILE}" ]]; then
    AWS_VAULT_PREFIX="aws-vault exec ${AWS_VAULT_PROFILE} --no-session -- "
fi

echo "=== Using the following parameters ==="
echo "Region: ${REGION}"
echo "Stage: ${STAGE}"
echo "Profile: ${AWS_VAULT_PROFILE}"

echo ""
echo "=== Applying action: ${TF_ACTION} ==="

pushd ./infrastructure
terraform init
aws_exec "terraform ${TF_ACTION} --var aws_region=${REGION} --var project_name=${NAME}"
popd

echo ""
echo "Completed."
