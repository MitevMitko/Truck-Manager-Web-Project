# Truck Management Web Project

A web-based application for managing trucks, trailers, garages, drivers, etc. for transportation companies.

## Technologies:

- Frontend: HTML, CSS, JavaScript (with Bootstrap framework);
- Backend: ASP.NET Core (with C#), Entity Framework Core (for database access);
- Database: SQL Server;
- Authentication and Authorization: ASP.NET Core Identity;
- Testing: NUnit (for unit testing).

## Dependencies:

- coverlet.collector (Version: 3.2.0);
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore (Version: 6.0.24);
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (Version: 6.0.24);
- Microsoft.AspNetCore.Identity.UI (Version: 6.0.24);
- Microsoft.AspNetCore.Mvc (Version: 2.2.0);
- Microsoft.EntityFrameworkCore.SqlServer (Version: 6.0.24);
- Microsoft.EntityFrameworkCore.Tools (Version: 6.0.24);
- Microsoft.NET.Test.Sdk (Version: 17.5.0);
- Microsoft.VisualStudio.Web.CodeGeneration.Design (Version: 6.0.16);
- Moq (Version: 4.20.70);
- NUnit (Version: 3.13.3);
- NUnit.Analyzers (Version: 3.6.1);
- NUnit3TestAdapter (Version: 4.4.2);
- SixLabors.ImageSharp (Version: 3.1.4).

## Architecture:

The project follows the Model-View-Controller (MVC) architectural pattern and includes components such as:

- **Models**: Contains classes that represent the tables in the database;
- **Repositories**: Contains business logic for interaction with the tables from the database;
- **UnitOfWork**: Pattern which is used to combine all repositories and the option for saving the changes in the database;
- **Services**: Contains business logic for interaction with the UnitOfWork pattern, UserManager and SignManager;
- **ViewModels**: Contains classes that represent the data to be displayed in views.
- **Controllers**: Acts as an intermediary between the view model and the view. It receives user requests, processes them and returns an appropriate response.
- **Views**: Represents the user interface (UI) of the application. It displays data to the user and sends user input to the controller for processing;

## Features

This project includes the features such as Add, Edit, Remove, GetAll, GetById and etc. for managing the following entities:

- BankContact;
- Engine;
- Garage;
- Image;
- Order;
- Trailer;
- Transmission;
- Truck;
- User.

# Seed roles in the database:

Check for role "administrator" and role "user" in the database is executed when project is started.
If the roles does not exist the roles will be seeded in the database.

## Usage:

In order to start the project:

1. Clone the project repository from GitHub;
2. Install needed dependencies;
3. Configure database connection string in appsettings.json;
4. Run database migrations to create/update the database;
5. Run the project to start the application.

## License

This project is licensed under the MIT license.
