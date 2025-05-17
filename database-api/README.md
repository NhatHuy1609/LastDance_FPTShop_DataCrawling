# Database API

### Setup Steps

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build
```

### Add Migrations
```bash
# Create migration to the Data/Migratins folder
dotnet ef migrations add create-laptops-table --output-dir Data/Migrations
```

## Running the Application

```bash
# Run with hot reload using HTTPS profile
dotnet watch --launch-profile "https"
```
