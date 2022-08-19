provider "aws" {
  region = var.aws_region
  # profile = <credentials_profile>
}

terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 3.0"
    }
  }
}
