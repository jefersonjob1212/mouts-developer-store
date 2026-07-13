# Instructions for execute this project

## Introduction
This file contains instructions for executing the project proposed for the assessment.

- [Backend](#backend)
- [Frontend](#frontend)

## Backend

### 1. Create base 
For create the tables in PostgreSQL Database, run this commands
```
cd template\backend
dotnet ef database update --project src\Ambev.DeveloperEvaluation.ORM --startup-project src\Ambev.DeveloperEvaluation.WebAPI
```
The database configuration is in appsettings.json.

### 2. Run app
For run app, use .NET Cli or open in Visual Studio.
If use .NET Cli, run this command
```
template\backend
dotnet run --project src\Ambev.DeveloperEvaluation.WebAPI
```
The system API run in port 5119

### 3. Run tests
For run test, use .NET Cli or open in Visual Studio.
If use .NET Cli, run this command
```
template\backend
dotnet test .\Ambev.DeveloperEvaluation.sln
```

## Frontend

### 1. Install dependencies
First, install Angular dependencies using npm
```
npm install
```

### 2. Run the app
For run app, use npm
```
npm start
```
The app runs on port 4200.

### 3. Test
For test, use the command
```
npm run test
```
The Karma page in browser shows the tests

## 4. The system
The system shows a sale page with paginated table. The filter has sale number, client and subsidiary for filter.
For create a new sale, click in New Sale Button and fill the fields for create a sale
In grid sale, you can edit and cancel the sale.
