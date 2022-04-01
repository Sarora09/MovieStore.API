# Project Owner : Sapan Arora

# Project Name: Movie Collection .Net Core Web API using the C# language

## About: 
This project is built using ASP.Net Core 5.0. I did this project to create an API that will provide the details requested by the client (react frontend) in this case, mainly in the form of JSON data. The client can perform a CRUD operation on the database based on the client's frontend inputs. CRUD stands for Create, Read, Update, and Delete operation. I have considered two actors for my website. The first actor is the Admin and the second actor is the customers. This project doesn't need the front end as the APIs can be tested using Postman. However, to see the utility of this app, I would suggest you refer to the MovieStore.WebApp on my Github repository and run both projects concurrently.

## Required Software to run the app:
1) Visual Studio to run the .Net core Web API
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
I have used Postman and my frontend React app to use this web API. For a better understanding of all the action methods, please refer to the MovieStore.WebApp on my Github repository.

# Areas of Improvement:

The app is in the development stage and is functional. However, the app requires iterations to improve. This app is for my learning purposes only.
