# Futurice Calculator

Futurice Calculator is a web API to implement a simple calculator.
This service offers an endpoint that reads a string input and parses it. It returns either an HTTP error code, or a solution to the calculation in JSON form.

An example calculus query:

**Original query:** `2 * (23/(3*3))- 23 * (2*3)`

**GET Endpoint:** `/calculator?query=[input]`

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

#### Swagger Page:

   https://b33z9ie8lh.execute-api.us-east-1.amazonaws.com/swagger/index.html

#### Get item:
   
```http
  GET /calculator?query={input}
```

| Parameter    | Type     | Description                                    |
| :----------- | :------- | :--------------------------------------------- |
| `query`      | `string` | **Required**. query to perform calculations on |

#### Evaluate expression:
```2 * (23/(3*3))- 23 * (2*3)```

https://b33z9ie8lh.execute-api.us-east-1.amazonaws.com/calculator?query=MSoyLTMvNCs1KjYtNyo4KzkvMTA=

## Project Architecture


   ![Architecture Diagram](https://github.com/dobhal1420/calculatorExpression/blob/dobhal1420-patch-1/Futurice_Calculator.jpeg)


## Code Flow

  * There are 3 sections of the code 
    - Controller
    - Model
    - Service

#### Controller:

  * *CalculatorController.cs* handles incoming HTTP requests and sends response back to the caller.
  * An HTTP get method is defined in the controller class.

#### Model:

  * A Model *CalcResponse.cs* represents data that is being transferred between controller components or any other related business logic.

#### Service:

  * This is the logical layer which aims at organizing business logic.
  * *ExpressionParserService.cs* is responsible for evaluating/parsing the query expression passed via GET method.
  * *LambdaFuturiceEntryPoint.cs* is a Lambda function handler responsible to integrating the web API with AWS Lambda function.

## Deployment Strategy

This project is deployed on AWS. Following services are created from AWS Console-:

1. **VPC**
   * CIDR block - 10.0.0.0/16
   * 2 Public Subnets - in 2 Availaibility Zones to build a highly available solution.
2. **API Gateway** 
   * HTTP API URL created in AWS API Gateway
3. **AWS Lambda**
   * Deployed Dot net core API to AWS Lambda

#### AWS VPC:

  * VPC is created on CIDR block 10.0.0.0/16
  * 2 route tables are availble inside this VPC
     - Main route table: This is default route table created by default when a VPC is created.
     - Custom route table: This custom route table is created to allow public subnets to connect to the internet
  * Security Groups
  * 2 Public Subnets created in 2 different availablility zones in order to make the system highly available and fault tolerant.

#### API Gateway:

  * API gateway is an API management tool that sits between a client and a collection of backend services.
  * HTTP API URL is created in AWS API Gateway to enable users to invoke our Lambda function via public URL.
  * This gateway connects with the 2 public subnets.
  * Routes are configured by declaring the following:
    - Method type: ANY
    - Resource Path: /{Proxy+}
      - [ANY /Proxy+] : is a greedy path variable that allows us to route to any action method(eg. /path1{input} , /path1/path2/{input}) with any HTTP method
    - Integration Target: Lamda Function
  * Routes direct incoming API requests to backend resources.


#### Lambda Function:

  ###### Dot net core solution is packaged and deployed on AWS Lambda using following steps:
  
  * Install the AWS Lambda Extensions for the dotnet CLI with the command dotnet tool. This tool enables you to package and deploy .NET Core applications to Lambda.
  ```
  install -g Amazon.Lambda.Tools
  ```

  * Run the below command to generate a zip file of the API that can be deployed to AWS Lambda (located in /bin/Release/netcoreapp3.1/FuturiceCalculator.zip). 
  ```
  dotnet lambda package
  ```
  
  * In the AWS Console, go to the Lambda Service section.
  * Click the link to the created lambda function.
  * Upload .zip file packaged (FuturiceCalculator.zip)
  * Under Runtime settings click Edit. 
  * Update the Handler to point to the created LambdaEntryPoint class (FuturiceCalculator::FuturiceCalculator.LambdaFuturiceEntryPoint::FunctionHandlerAsync).


## Tests

  * The *xUnit* testing framework has been used for integration/unit test.
  * *Moq* framework has been used for mocking the service.
  * This framework includes following packages of classes that provides support for developing and executing unit tests:
    - *CalculatorControllerTest.cs*
      -  TestWhenGetAllCalculatorControllerNullQueryParameter - This method validates the test case when null value is passed from GET method.
      -  TestWhenGetAllCalculatorControllerExpressionQueryParameter - This method sends the response when a valid string/expression is passed from GET method.
    - *ExpressionParserServiceTest.cs*
      - TestNegativeDataIncorrectFormatExpressionParserService - This method validates incorrect format of the expression such as "2/0"
      - TestNegativeDataVariableExpressionParserService - This method validates a scenario when the expression cannot be evaluated. eg. "a+b"
      - TestNegativeDataSpecialCharacterExpressionParserService - This method validates a scenario when expression contains a syntax error
      - TestNullArgumentDataExpressionParserService - This method validates a scenario when null value is passed in the expression
  * *TestDataGenerationDataSet.cs* is a test data class that contains all the sample mathematical expressions with expected outcome.
