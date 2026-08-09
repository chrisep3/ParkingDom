# ParkingDom — ASP.NET Core MVC Parking Reservation System

Have you ever been driving around a busy area, going in circles trying to find a parking spot, only to end up giving up and pulling into a private parking facility?

ParkingDom aims to make that whole experience easier. It explores a simple solution through a parking-reservation application focused on one of the busiest suburbs of Athens — Chalandri.

Users can browse participating private parking facilities, view their details and reserve a parking space ahead of time. Parking owners have a separate role and can securely manage the availability of their own facilities.

> 🎥 **Prefer to see the project in action?**  
> Watch the [ParkingDom Project Walkthrough](https://youtu.be/zr0dyZh8YYI) for a complete application demonstration and a technical explanation of the implementation. Detailed chapters are available in the [Video walkthrough](#video-walkthrough) section below.

## Features

- User registration, login and logout
- Separate `User` and `Owner` roles
- Browsing of available parking facilities
- Creation, viewing and deletion of reservations
- Parking capacity and availability management
- Detection of overlapping reservations
- Relational data using foreign keys
- Razor Views styled with Bootstrap
- Asynchronous database access using LINQ and `async/await`

## Authentication, authorization and validation

### Authentication

- ASP.NET Core Identity manages user registration, password hashing and sign-in.
- After a successful login, an authentication cookie identifies the signed-in user in subsequent requests.
- Unauthenticated users are redirected to the application’s login page.
- Demo Owner accounts are created during application startup.

### Authorization

- Role-based authorization separates regular users from parking owners.
- Reservation actions are restricted to authenticated users with the `User` role.
- Parking-management actions are restricted to authenticated users with the `Owner` role.
- Each user can view and delete only their own reservations.
- Each owner can manage only the parking facility associated with their Identity user ID.
- Ownership is verified through database queries, preventing users from accessing another user’s data by manually changing the URL.

### Validation

- Registration and reservation forms use model validation.
- Password confirmation is checked during registration.
- Reservations cannot begin in the past.
- The reservation start time must be earlier than the end time.
- The selected parking facility must exist.
- Overlapping reservations are counted before a reservation is accepted.
- A reservation is rejected when the selected parking facility has reached its capacity for that time period.
- Owners cannot enter a reserved-spots value below zero or above their parking facility’s total capacity.

## Technologies

- C#
- .NET 8
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- SQLite
- Razor Views
- Bootstrap
- LINQ
- Async/await

## Project structure

```text
Controllers/       MVC controllers and application request handling
Models/            Domain models, Identity user and view models
Views/             Razor Views organised by controller
Migrations/        Entity Framework Core database migrations
wwwroot/           CSS, JavaScript, Bootstrap and static assets
AppDbContext.cs    Entity Framework Core and Identity database context
Program.cs         Application configuration, services and demo-data seeding
```

## Running the project locally

### Prerequisites

Before running the application, make sure that the following are installed:

- [Git](https://git-scm.com/)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- The **ASP.NET and web development** Visual Studio workload

SQLite is used as the database, so a separate database server is not required.

### 1. Clone the repository

Open PowerShell or another terminal and run:

```powershell
git clone https://github.com/chrisep3/ParkingDom.git
cd ParkingDom
```

Alternatively, download the repository as a ZIP file from GitHub and extract it locally.

### 2. Open the solution

Open the following file in Visual Studio 2022:

```text
Parking.sln
```

Visual Studio should automatically restore the required NuGet packages.

If package restoration does not begin automatically:

1. Right-click the solution in Solution Explorer.
2. Select **Restore NuGet Packages**.
3. Build the solution once to verify that all dependencies are available.

### 3. Create the local database

In Visual Studio, open:

```text
Tools → NuGet Package Manager → Package Manager Console
```

Make sure that `Parking` is selected as the default project and execute:

```powershell
Update-Database
```

This applies the included Entity Framework Core migrations and creates the local SQLite database:

```text
parking.db
```

The database file is created locally and is excluded from version control.

### 4. Run the application

Select the HTTPS launch profile and run the application using:

```text
F5
```

or:

```text
Ctrl + F5
```

Visual Studio will start the application and open it in your default browser. The exact local address is displayed by Visual Studio and may look similar to:

```text
https://localhost:7000
```

### 5. Test the regular User workflow

1. Open the registration page.
2. Create a new user account.
3. Sign in using the newly created account.
4. Browse the available parking facilities.
5. Select a parking facility and create a reservation.
6. View your reservations.
7. Delete a reservation.

Regular users are automatically assigned to the `User` role during registration.

### 6. Test the Owner workflow

Use the following locally seeded demo account:

```text
Email: owner1parking@gmail.com
Password: Password123!
```

After signing in, the Owner can access the management page for the parking facility associated with that account.

The Owner can change its availability but cannot manage another owner’s parking facility.

> The demo credentials are intended exclusively for local portfolio testing and do not provide access to any external service.

## Troubleshooting

### `Update-Database` is not recognised

Confirm that:

- The command is being executed inside Visual Studio’s **Package Manager Console**.
- `Parking` is selected as the default project.
- The solution has been restored and built successfully.

### NuGet packages are missing

Right-click the solution and select:

```text
Restore NuGet Packages
```

Then rebuild the solution.

### HTTPS certificate warning

If the browser does not trust the local ASP.NET Core development certificate, run:

```powershell
dotnet dev-certs https --trust
```

Restart Visual Studio and run the application again.

### The database does not contain the demo accounts

Confirm that:

1. `Update-Database` completed successfully.
2. The `parking.db` file exists.
3. The application has been started at least once.

The roles, demo Owner accounts and their parking facilities are created during application startup.

## Video walkthrough

A complete demonstration and technical walkthrough of the project is available on YouTube:

[Watch the ParkingDom Project Walkthrough](https://youtu.be/zr0dyZh8YYI)

### Video chapters

```text
00:00 Introduction and the problem ParkingDom addresses
01:18 Application demonstration
05:33 Visual Studio technical walkthrough
18:15 Future improvements and next steps
```

## About the project

ParkingDom was developed as part of my transition from physics and teaching into software development and is included in my personal portfolio.

Through its development, I gained practical experience with:

- MVC architecture
- ASP.NET Core Identity
- Authentication cookies
- Role-based authorization
- Entity Framework Core
- Relational database design
- Input and business-rule validation
- Secure ownership checks
- LINQ and asynchronous database access

The project currently represents a focused portfolio implementation. Potential future improvements include real-world parking data, maps, online payments, automated reservation expiration and a more advanced availability system.
