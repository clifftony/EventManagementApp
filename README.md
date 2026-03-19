# Event Management Application

## Overview
This is an Event Management Web Application built using ASP.NET Core MVC. It provides a platform for users to create, manage, and view events. The application incorporates user authentication, event CRUD operations, and a responsive design using Bootstrap.

## Project Structure
The project is organized into several folders, each serving a specific purpose:

- **Controllers**: Contains the logic for handling user requests and responses.
  - `HomeController.cs`: Manages the home page.
  - `EventsController.cs`: Handles CRUD operations for events.
  - `AccountController.cs`: Manages user authentication.

- **Models**: Defines the data structures used in the application.
  - `Event.cs`: Represents an event with properties like Title, Description, Date, Time, and Location.
  - `ApplicationUser.cs`: Extends IdentityUser for user management.
  - `ViewModels`: Contains view models for creating and editing events and user login.

- **Views**: Contains Razor views for rendering HTML.
  - `Shared`: Includes layout and partial views.
  - `Home`: Contains the home page view.
  - `Events`: Contains views for listing, creating, editing, and viewing event details.
  - `Account`: Contains views for user login and registration.

- **Data**: Manages database context and migrations.
  - `ApplicationDbContext.cs`: Configures the database connection and includes DbSets for events and users.
  - `Migrations`: Contains migration files for database schema changes.

- **wwwroot**: Contains static files like CSS.
  - `css/site.css`: Custom styles for the application.

- **Configuration Files**: 
  - `appsettings.json`: Contains configuration settings for the application.
  - `Program.cs`: Entry point for configuring services and middleware.
  - `EventManagementApp.csproj`: Project file with dependencies and build settings.

## Setup Instructions
1. Clone the repository.
2. Navigate to the project directory.
3. Run `dotnet restore` to install dependencies.
4. Run `dotnet ef database update` to create the database.
5. Run `dotnet run` to start the application.

## Test Credentials
- Register a new user via the registration page.
- Use the registered credentials to log in.

## MVC Architecture
- **Model**: Represents the data structure (Event and ApplicationUser).
- **View**: Displays the user interface (Razor views).
- **Controller**: Handles user input and interactions (EventsController and AccountController).

## Features
- User authentication using ASP.NET Identity.
- Full CRUD functionality for events.
- Responsive design with Bootstrap.
- Validation for user input and event details.

## Acknowledgments
This project demonstrates the use of ASP.NET Core MVC architecture, Entity Framework Core for data access, and Razor views for dynamic content rendering.