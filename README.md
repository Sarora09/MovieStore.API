# Project Owner : Sapan Arora

# Project Name: Movie Collection .NET Core RESTful Web API using the C# language

## About: 

The project is a RESTful Web API project deployed on Azure. Please refer to the [website](https://movie-collection-api-app.azurewebsites.net/) for the Swagger documentation. It is built using ASP.NET Core 5.0. I did this project to create an API that will provide the details requested by the client (react frontend) in this case, mainly in the form of JSON data. The client can perform a CRUD operation on the database based on the client's frontend inputs. CRUD stands for Create, Read, Update, and Delete operation.

The project can be tested using the Postman or [Swagger](https://movie-collection-api-app.azurewebsites.net/). However, to see the utility of this app, you may refer to the [MovieStore.WebApp project](https://github.com/Sarora09/MovieStore.WebApp) on my Github repository and run both projects concurrently.

I have created two API services, i.e., Access API service and Movies API service:

- Access API uses the Access controller, IAccessRepository interface (dependency injection), and AccessRepository to create, read, update, and delete a user according to the incoming request.

- Movies API uses the Movies controller, IMovieRepository interface (dependency injection), and MovieRepository to create, read, update, and delete a movie according to the incoming request.

## Required Software to run the app:
1) Visual Studio to run the .NET Core Web API
2) Postman application to test the API
3) Microsoft SQL Server

## Steps to run the app:

### Creating tables:
You would need to perform the following:
1) Download the project and open the project using the visual studio.
2) Open the NuGet package manager console.
3) At this point, you should have the migration files when you downloaded this project. To create the tables from those migration files, you need to write in the NuGet package manager console "update-database". It will create the database in your SQL Server.

### Run the Server
You can run the server in two modes:
1) Press F5 to run the server in debug mode.
2) Press Ctrl+F5 to run the server without debug mode.

## Note:
I have used Postman and my frontend React app to use this web API. For a better understanding of all the action methods, please refer to the [MovieStore.WebApp project](https://github.com/Sarora09/MovieStore.WebApp) on my Github repository.

# Areas of Improvement:

This app is in the development stage and is functional. However, the app requires iterations to improve. Work is in progress. This app is for my learning purposes only.
