# Employee Management System

ASP.NET Core MVC application for managing employees and departments with Clean Architecture.

## Prerequisites

- .NET 8.0 SDK
- Docker (for containerized deployment)

## Build/Run Instructions

### Docker (Recommended)

```bash
git clone https://github.com/michaeleliyahu/EmployeeManagementSystem.git
cd EmployeeManagementSystem
docker-compose up --build
```

Access: http://localhost:8080

### Local Development

```bash
dotnet restore
dotnet build
dotnet run
```

Access: http://localhost:5151

## Architecture Rationale

**Clean Architecture** with clear separation of concerns:

```
├── Models/         # Domain entities
├── Repositories/   # Data access layer (JSON files)
├── Services/      # Business logic layer
├── Controllers/   # Presentation layer
└── Views/         # UI layer
```

**Benefits:**
- **Testability**: Business logic isolated from infrastructure
- **Maintainability**: Changes in one layer don't affect others
- **Scalability**: Easy to replace JSON storage with database

**Technology Stack:**
- ASP.NET Core 8.0 MVC
- JSON file storage with thread-safe operations
- Bootstrap 5 responsive UI
- Docker containerization
