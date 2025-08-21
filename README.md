# Employee Management System

A comprehensive ASP.NET Core MVC application for managing employees and departments with Clean Architecture design patterns.

## 🚀 Features

- **Employee Management**: Full CRUD operations for employees
- **Department Management**: Manage departments with business rules
- **Dashboard**: Real-time statistics and search functionality
- **Data Validation**: Comprehensive server-side validation
- **File-based Storage**: JSON-based persistence with file locking
- **Exception Handling**: Global error handling with structured logging
- **Responsive Design**: Bootstrap-powered responsive UI
- **Docker Support**: Containerized deployment

## 🏗️ Architecture

This application follows **Clean Architecture** principles with clear separation of concerns:

```
├── Models/              # Domain entities (Employee, Department)
├── Repositories/        # Data access layer (JSON file operations)
├── Services/           # Business logic layer
├── Controllers/        # Presentation layer
├── Views/             # Razor view templates
├── App_Data/          # JSON data storage
└── Logs/              # Application logs
```

### Architecture Benefits

- **Separation of Concerns**: Each layer has a specific responsibility
- **Testability**: Business logic is isolated and easily testable
- **Maintainability**: Changes in one layer don't affect others
- **Scalability**: Easy to replace JSON storage with database later

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0 MVC
- **Language**: C# 12
- **Storage**: JSON files with file locking
- **UI**: Bootstrap 5, Razor Views
- **Containerization**: Docker
- **Validation**: Data Annotations
- **Logging**: Built-in ASP.NET Core logging

## 📋 Prerequisites

- .NET 8.0 SDK
- Docker (for containerized deployment)
- Visual Studio 2022 or VS Code (recommended)

## 🚀 Quick Start

### Option 1: Docker (Recommended)

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EmployeeManagementSystem
   ```

2. **Run with Docker Compose**
   ```bash
   docker-compose up --build
   ```

3. **Access the application**
   - Open browser: http://localhost:8080
   - Default dashboard: http://localhost:8080/Dashboard

### Option 2: Local Development

1. **Prerequisites**
   ```bash
   dotnet --version  # Should be 8.0+
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   - Open browser: https://localhost:7032 or http://localhost:5555

## 📁 Data Storage

The application uses JSON files for data persistence:

- `App_Data/employees.json` - Employee records
- `App_Data/departments.json` - Department records
- `Logs/` - Application logs (created automatically)

### Data Structure

**Employee**:
```json
{
  "Id": 1,
  "FirstName": "John",
  "LastName": "Doe",
  "Email": "john.doe@company.com",
  "HireDate": "2024-01-15T00:00:00",
  "Salary": 75000.00,
  "DepartmentId": 1
}
```

**Department**:
```json
{
  "Id": 1,
  "Name": "Engineering"
}
```

## 🎯 Business Rules

### Employee Rules
- Email must be valid and unique
- Salary must be between $30,000 and $200,000
- First name and last name are required
- Must be assigned to an existing department

### Department Rules
- Department name is required (max 100 characters)
- Department cannot be deleted if it has employees
- Department names should be unique (validation in service layer)

### Dashboard Rules
- Shows employees grouped by department
- Displays recent hires (last 30 days)
- Supports search by name and department filter
- Real-time statistics calculation

## 🔒 Security Features

- **Anti-Forgery Tokens**: CSRF protection on all forms
- **Input Validation**: Server-side validation on all inputs
- **SQL Injection Prevention**: No SQL used (JSON storage)
- **XSS Protection**: Razor automatically encodes output
- **File Locking**: Prevents data corruption during concurrent access

## 🧪 Testing the Application

### Creating Test Data

1. **Add Departments**:
   - Go to http://localhost:8080/Departments
   - Click "Create New"
   - Add departments: "Engineering", "Marketing", "HR"

2. **Add Employees**:
   - Go to http://localhost:8080/Employees
   - Click "Create New"
   - Fill in employee details and assign to departments

3. **View Dashboard**:
   - Go to http://localhost:8080/Dashboard
   - See statistics and recent hires
   - Test search functionality

### Testing Delete Operations

- Click any "Delete" button
- Confirm the JavaScript confirmation dialog
- Verify the record is removed
- Try deleting a department with employees (should show error)

## 🚢 Deployment

### Docker Deployment

The application includes a multi-stage Dockerfile:

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "EmployeeManagementSystem.dll"]
```

### Production Considerations

For production deployment, consider:

1. **Database Migration**: Replace JSON storage with SQL Server/PostgreSQL
2. **Authentication**: Add Identity framework for user management
3. **HTTPS**: Configure SSL certificates
4. **Caching**: Add Redis for better performance
5. **Monitoring**: Implement application monitoring (Application Insights)
6. **Backup**: Implement data backup strategies

## 🐛 Troubleshooting

### Common Issues

1. **Port Already in Use**
   ```bash
   # Check what's using port 8080
   netstat -ano | findstr :8080
   # Kill the process or change port in docker-compose.yml
   ```

2. **File Access Errors**
   - Ensure App_Data directory exists and is writable
   - Check file permissions if running on Linux/Mac

3. **Docker Build Fails**
   ```bash
   # Clean Docker cache
   docker system prune -a
   # Rebuild without cache
   docker-compose build --no-cache
   ```

### Log Analysis

Application logs are stored in the `Logs/` directory:
- Check exception logs for detailed error information
- Log files are created daily with format: `log-YYYYMMDD.txt`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 API Endpoints

### Employee Endpoints
- `GET /Employees` - List all employees (with pagination)
- `GET /Employees/Create` - Show create form
- `POST /Employees/Create` - Create new employee
- `GET /Employees/Edit/{id}` - Show edit form
- `POST /Employees/Edit/{id}` - Update employee
- `POST /Employees/Delete/{id}` - Delete employee

### Department Endpoints
- `GET /Departments` - List all departments
- `GET /Departments/Create` - Show create form
- `POST /Departments/Create` - Create new department
- `GET /Departments/Edit/{id}` - Show edit form
- `POST /Departments/Edit/{id}` - Update department
- `POST /Departments/Delete/{id}` - Delete department

### Dashboard Endpoints
- `GET /Dashboard` - Show dashboard with statistics
- `GET /Dashboard?searchName=John&searchDeptId=1` - Filtered dashboard

## 📊 Performance Features

- **Pagination**: Employee list supports pagination (10 records per page)
- **File Locking**: Thread-safe JSON file operations
- **Lazy Loading**: Views only load required data
- **Efficient Queries**: In-memory filtering for small datasets

## 🔮 Future Enhancements

- [ ] Database integration (Entity Framework Core)
- [ ] User authentication and authorization
- [ ] Employee photo upload
- [ ] Advanced reporting (Excel export)
- [ ] Email notifications
- [ ] Audit trail for changes
- [ ] API endpoints (REST/GraphQL)
- [ ] Real-time updates (SignalR)
- [ ] Advanced search and filtering
- [ ] Employee performance tracking

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Author

Developed as a demonstration of Clean Architecture principles in ASP.NET Core MVC.
