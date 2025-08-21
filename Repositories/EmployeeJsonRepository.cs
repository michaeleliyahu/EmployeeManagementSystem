using System.Text.Json;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeJsonRepository : IEmployeeRepository
    {
        private readonly string _filePath = Path.Combine("App_Data", "employees.json");
        private readonly object _lock = new object();

        public EmployeeJsonRepository()
        {
            // Ensure App_Data directory exists
            Directory.CreateDirectory("App_Data");
            
            // Initialize file if it doesn't exist
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Employee> GetAll()
        {
            lock (_lock)
            {
                try
                {
                    var json = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
                }
                catch (Exception)
                {
                    return new List<Employee>();
                }
            }
        }

        public Employee? GetById(int id)
        {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            lock (_lock)
            {
                var employees = GetAll();
                employee.Id = employees.Count > 0 ? employees.Max(e => e.Id) + 1 : 1;
                employees.Add(employee);
                SaveToFile(employees);
            }
        }

        public void Update(Employee employee)
        {
            lock (_lock)
            {
                var employees = GetAll();
                var existingEmployee = employees.FirstOrDefault(e => e.Id == employee.Id);
                if (existingEmployee != null)
                {
                    employees.Remove(existingEmployee);
                    employees.Add(employee);
                    SaveToFile(employees);
                }
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                var employees = GetAll();
                var employee = employees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    employees.Remove(employee);
                    SaveToFile(employees);
                }
            }
        }

        private void SaveToFile(List<Employee> employees)
        {
            try
            {
                var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save employees data", ex);
            }
        }
    }
}
