# Quick Start Guide

## How to Run the Application

### Using Docker (Recommended - Easy Setup)

1. **Open Command Prompt/Terminal** and navigate to the project folder:
   ```
   cd "c:\Users\Michael Eliyahu\Downloads\EmployeeManagementSystem"
   ```

2. **Start the application:**
   ```
   docker-compose up --build
   ```

3. **Open your browser** and go to:
   - **Main Application**: http://localhost:8080
   - **Dashboard**: http://localhost:8080/Dashboard
   - **Employees**: http://localhost:8080/Employees
   - **Departments**: http://localhost:8080/Departments

4. **To stop the application**, press `Ctrl+C` in the terminal

### Using .NET (Alternative)

1. **Make sure you have .NET 8.0 installed**
2. **Navigate to project folder and run:**
   ```
   dotnet run
   ```
3. **Open browser** to the URL shown in terminal (usually https://localhost:7032)

## Sample Data Included

The application comes with sample data:
- **Departments**: HR, Engineering, Marketing, Finance, Sales  
- **Employees**: Sample employees in different departments
- You can add, edit, and delete records through the web interface

## Key Features to Test

1. **Dashboard** - View statistics and search employees
2. **Add New Employee** - Click "Create New" in Employees section
3. **Add New Department** - Click "Create New" in Departments section
4. **Delete with Confirmation** - Try deleting records (you'll see a confirmation dialog)
5. **Business Rules** - Try deleting a department that has employees (it will prevent you)

## Troubleshooting

- **Port 8080 in use?** Change the port in `docker-compose.yml` file
- **Docker not installed?** Use the .NET method instead
- **Need help?** Check the full README.md for detailed information
