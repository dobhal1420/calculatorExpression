# Futurice Calculator

Futurice Calculator is a web API to implement a simple calculator.
This service offers an endpoint that reads a string input and parses it. It returns either an HTTP error code, or a solution to the calculation in JSON form.

An example calculus query:

**Original query:** `2 * (23/(3*3))- 23 * (2*3)`

**GET Endpoint:** `/calculus?query=[input]`

Application Web API is designed in Asp Dot Net core and deployed in AWS leveraging following technology-:
  * **AWS VPC** - For newtwork security
  * **API Gateway** - For single endpoint URL
  * **Lambda Function** - For scalability and reliability

## Contents
  * [API Reference](#api-reference)
  * [Project Architecture](#project-architecture)
  * [Code Flow](#code-flow)
  * [Deployment Strategy](#deployment-strategy)
  * [Tests](#tests)


## API Reference

#### Swagger:

#### Get item:
   
```http
  GET /calculus?query={input}
```

| Parameter    | Type     | Description                                    |
| :----------- | :------- | :--------------------------------------------- |
| `query`      | `string` | **Required**. query to perform calculations on |

#### Evaluate expression:
'2 * (23/(3*3))- 23 * (2*3)'

## Project Architecture


   ![Architecture Diagram](https://github.com/dobhal1420/calculatorExpression/blob/dobhal1420-patch-1/Futurice_Calculator.jpeg)


## Code Flow

## Deployment Strategy

This project is deployed on AWS. Following services are created from AWS Console-:

1. **VPC**
   * CIDR block - 10.0.0.0/16
   * 2 Public Subnets - in 2 Availaibility Zones to build a highly availaibile solution.
2. **API Gateway** 
   * HTTP API created in AWS API Gateway
3. **AWS Lambda**
   * Deployed Dot net core Api to AWS Lambda

#### Create AWS VPC:

  * VPC is created on CIDR block 10.0.0.0/16
  * 2 route tables are availble inside this VPC
     - Main route table: This is default route table created by default when a VPC is created.
     - Custom route table: This custom route table is created to allow public subnets to connect to the internet
  * Security Groups
  * 2 Public Subnets created in 2 different availablility zones in order to make the system highly available and fault tolerant.

#### Create API Gateway:

  * The API solution created in Dot Net Core is deployed on API Gateway.
  * This gateway connects with the 2 public subnets which further connects to the internet via Internet Gateway

#### Create Lambda Function:

  * Install the AWS Lambda Extensions for the dotnet CLI with the command dotnet tool. This tool enables you to package and deploy .NET Core applications to Lambda.
  ```
  install -g Amazon.Lambda.Tools
  ```

  * Run the below command to generate a zip file of the API that can be deployed to AWS Lambda (located in /bin/Release/netcoreapp3.1/FuturiceCalculator.zip). 
  ```
  dotnet lambda package
  ```
  
  * In the AWS Console, go to the Lambda Service section.
  * Click the link to the lambda function you created above (dotnet-lambda-futurice).
  * Under Code source click Upload from and select .zip file.
  * Click Upload, select the application zip file you generated above (/bin/Release/netcoreapp3.1/FuturiceCalculator.zip) and click Save.
  * Under Runtime settings click Edit.Update the Handler to point to the LambdaEntryPoint class you created above (FuturiceCalculator::FuturiceCalculator.LambdaFuturiceEntryPoint::FunctionHandlerAsync).
  * Click Save.

## Tests
