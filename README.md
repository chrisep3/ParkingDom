# ParkDom — ASP.NET Core MVC Parking Reservation System

ParkDom is a web application for parking reservations, built with ASP.NET Core MVC, ASP.NET Core Identity, Entity Framework Core and SQLite.

Users can create an account, browse available parking facilities and make reservations. Parking owners have a separate role and can manage the availability of their own parking facility.

## Features

- User registration, login and logout
- Separate `User` and `Owner` roles
- Creation, viewing and deletion of reservations
- Parking availability and capacity management
- Detection of overlapping reservations
- Relational data using foreign keys
- Razor Views styled with Bootstrap
- Asynchronous database access with LINQ and `async/await`

## Authentication, authorization and validation

### Authentication

- ASP.NET Core Identity manages user registration, password hashing and sign-in.
- After a successful login, an authentication cookie identifies the signed-in user in subsequent requests.
- Login and access-denied requests are redirected to the application’s login page.
- Seeded Owner accounts are assigned to the `Owner` role during application startup.

### Authorization

- Role-based authorization separates regular users from parking owners.
- Reservation actions are restricted to users with the `User` role.
- Parking-management actions are restricted to users with the `Owner` role.
- Database queries include ownership checks, preventing users from accessing another user’s reservation by changing the URL.
- Each owner can manage only the parking facility associated with their Identity user ID.

### Validation

- Registration and reservation forms use model validation.
- Password confirmation is checked during registration.
- Reservations cannot start in the past.
- The start time must be earlier than the end time.
- The selected parking facility must exist.
- Overlapping reservations are counted before a reservation is accepted.
- A reservation is rejected when the parking facility has reached its capacity for the selected period.
- Owners cannot enter a reserved-spots value below zero or above the parking facility’s total capacity.

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

Through this project, I gained practical experience with MVC architecture, ASP.NET Core Identity, authentication cookies, role-based authorization, Entity Framework Core, relational data, validation and secure ownership checks.