# Instructions for execute this project

## Introduction
This file contains instructions for executing the project proposed for the assessment.

[Backend](#backend)

## Backend

### 1. 
For create the tables in PostgreSQL Database, run this commands
```
cd template\backend
dotnet ef database update --project src\Ambev.DeveloperEvaluation.ORM --startup-project src\Ambev.DeveloperEvaluation.WebAPI
```