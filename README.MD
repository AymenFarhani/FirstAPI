FirstAPI
A simple ASP.NET Core Web API project demonstrating fundamental concepts such as controllers, models, validation, and data persistence.

***Technologies Used***
- .NET 8.0: Latest version of the .NET platform for building cross-platform applications.
- ASP.NET Core Web API: Framework for building RESTful APIs with .NET.
- Entity Framework Core: Object-Relational Mapper (ORM) for data access.
- SQLServer databasee: Relational database for data storage.
- FluentValidation: Library for building strongly-typed validation rules.
- Dependency Injection: Built-in support for managing dependencies and promoting modular architecture.
- Configuration Files: Utilizes appsettings.json and appsettings.Development.json for environment-specific settings.
- Basic security Authentication process 
- Containerization: shipping and deploying the application using Docker
- .editorconfig: Defines consistent coding styles across the project.
- Visual Studio Solution (.sln): Organizes project files for development in Visual Studio.

***Project Structure***
- Controllers/: API controllers handling HTTP requests.
- Models/: Data models representing the application's entities.
- Validators/: FluentValidation classes for model validation.
- Persistence/: Data context and database-related configurations.
- Exceptions/: Custom exception classes for error handling.
- Filters/: Action filters for cross-cutting concerns like logging and error handling.
- Authentication: Basic security process
- Properties/: Project properties and settings.
- Dockerfile and docker-compose.yml files for the shipping and deployment

***Getting Started***
Clone the repository:
- git clone https://github.com/AymenFarhani/FirstAPI.git

Navigate to the project directory:
- cd FirstAPI

Restore dependencies:
- dotnet restore

Build the project:
- dotnet build

Run the application:
- dotnet run

Access the API:
Navigate to https://localhost:8081 in your browser or API testing tool.

