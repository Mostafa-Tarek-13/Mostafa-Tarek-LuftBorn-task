# Mostafa-Tarek-LuftBorn-task

## Prerequisites

To run this project on your machine, you need the following non-trivial prerequisites:

- .NET SDK 10.0 or later, because the application targets .NET 10.0.
- PostgreSQL 15 or 16, since the project uses Npgsql with a PostgreSQL database.
- A PostgreSQL database named TaskDb.
- PostgreSQL credentials matching the connection string in the application configuration:
  - Host: localhost
  - Port: 5433
  - Username: postgres
  - Password: password

If you use different PostgreSQL settings, update the connection string in [Task/appsettings.json](Task/appsettings.json) and [Task/appsettings.Development.json](Task/appsettings.Development.json).

## Run locally

1. Create the TaskDb database in PostgreSQL.
2. Restore the dependencies:
   ```bash
   dotnet restore
   ```
3. Start the application:
   ```bash
   dotnet run --project Task
   ```

The API will start and initialize the database on first run.