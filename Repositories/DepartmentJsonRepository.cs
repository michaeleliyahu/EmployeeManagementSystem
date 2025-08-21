using System.Text.Json;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public class DepartmentJsonRepository : IDepartmentRepository
    {
        private readonly string _filePath = Path.Combine("App_Data", "departments.json");
        private readonly string _employeesFilePath = Path.Combine("App_Data", "employees.json");
        private readonly object _lock = new object();

        public DepartmentJsonRepository()
        {
            // Ensure App_Data directory exists
            Directory.CreateDirectory("App_Data");
            
            // Initialize file if it doesn't exist
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Department> GetAll()
        {
            lock (_lock)
            {
                try
                {
                    var json = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<Department>>(json) ?? new List<Department>();
                }
                catch (Exception)
                {
                    return new List<Department>();
                }
            }
        }

        public Department? GetById(int id)
        {
            return GetAll().FirstOrDefault(d => d.Id == id);
        }

        public void Add(Department department)
        {
            lock (_lock)
            {
                var departments = GetAll();
                department.Id = departments.Count > 0 ? departments.Max(d => d.Id) + 1 : 1;
                departments.Add(department);
                SaveToFile(departments);
            }
        }

        public void Update(Department department)
        {
            lock (_lock)
            {
                var departments = GetAll();
                var existingDepartment = departments.FirstOrDefault(d => d.Id == department.Id);
                if (existingDepartment != null)
                {
                    departments.Remove(existingDepartment);
                    departments.Add(department);
                    SaveToFile(departments);
                }
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                var departments = GetAll();
                var department = departments.FirstOrDefault(d => d.Id == id);
                if (department != null)
                {
                    departments.Remove(department);
                    SaveToFile(departments);
                }
            }
        }

        public bool HasEmployees(int departmentId)
        {
            try
            {
                if (!File.Exists(_employeesFilePath))
                    return false;

                var json = File.ReadAllText(_employeesFilePath);
                var employees = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
                return employees.Any(e => e.DepartmentId == departmentId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SaveToFile(List<Department> departments)
        {
            try
            {
                var json = JsonSerializer.Serialize(departments, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save departments data", ex);
            }
        }
    }
}
