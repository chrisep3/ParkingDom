# ParkDom — ASP.NET Core MVC Parking Reservation System

ParkDom is a web application for parking reservations, built with ASP.NET Core MVC, ASP.NET Core Identity, Entity Framework Core and SQLite.

Users can create an account, browse available parking facilities and make reservations. Parking owners have a separate role and can manage the availability of their own parking facility.

## Features

- Authentication and authorization with ASP.NET Core Identity
- Separate `User` and `Owner` roles
- Creation, viewing and deletion of reservations
- Validation of reservation dates and parking capacity
- Detection of overlapping reservations
- Role-based access control
- Ownership checks for reservations and parking facilities
- Relational data using foreign keys
- Razor Views styled with Bootstrap
- Asynchronous database access with LINQ and `async/await`

## User and Owner access

- Users register and sign in through the application.
- Each user can access only their own reservations.
- Parking owners sign in through dedicated Owner accounts.
- Each owner can manage only the parking facility associated with their account.

## Technologies

- C# and .NET 8
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- SQLite
- Razor Views
- Bootstrap
- LINQ
- Async/await

## Running the project

1. Clone the repository.
2. Open `Parking.sln` in Visual Studio 2022.
3. Restore the NuGet packages.
4. Open the Package Manager Console and run:

```powershell
Update-Database
```

5. Run the application through Visual Studio.

The SQLite database is created locally and is excluded from version control.

## Demo Owner account

For local testing:

```text
Email: owner1parking@gmail.com
Password: Password123!
```

These credentials are intended only for the locally seeded demo account.

## About the project

ParkDom was developed as part of my transition into software development and is included in my personal portfolio.

Through this project, I gained practical experience with MVC architecture, Identity, role-based authorization, Entity Framework Core, relational data, validation and secure ownership checks.