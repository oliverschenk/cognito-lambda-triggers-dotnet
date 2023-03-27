
variable "aws_region" {
  type        = string
  description = "The AWS region in which the resources will be deployed"
}

variable "project_name" {
  type        = string
  description = "The name of this project to be used as a prefix for resource names"
}

variable "stage" {
  type        = string
  description = "The environment stage"
  default     = "dev"
}
