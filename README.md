# My Rental

This project is created to help User to find, put on sale or rent a house, apartment.  
In this application User can manage his own or look at someone else's advertisements. Also User can search advertisements using the filter.

## What's inside

### Web Api  (Backend)
Web Api project contains all necessary endpoints to use the backend application.  
It can be used if you want to create your own frontend application.

### Blazor Server (Frontend + Backend)
Blazor project contains ready to go web-site  with all functionality of application.

## Installation

### What must be installed
- .NET 6
- Microsoft SQL Server
  - Created database

### How to install and run
1) Download/Clone the solution from repository
    - If download make sure to have the solution directory unarchived
2) Open solution directory in terminal
3) Run command:  
   `dotnet user-secrets set "ConnectionStrings:MyRentalDatabase" "<your_connection_string>" -p <startup_project>`  
   where:
    - `<your_connection_string>` - Connection String to your Microsoft SQL Server database
    - `<startup_project>` - Naming of the startup project you want to run. Watch possible values below
4) Be sure your database is run
5) Run command:  
   `dotnet run --project <startup_project>`  
   where `<startup_project>` - Naming of the startup project you want to run. Watch possible values below

> `<startup_project>` possible values:
> - `MyRental.Api` - Web Api (backend)
> - `MyRental.Blazor` - Blazor Server (frontend + backend)

## What technologies were used

- .NET 6 + ASP.NET Core
- Blazor Server-Side + HTML + CSS
- Microsoft SQL Server
- Entity Framework Core + Identity
- Fluent Validator
- AutoMapper  
