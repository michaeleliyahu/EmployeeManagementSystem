using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;

namespace EmployeeManagementSystem.Services
{
    public class DashboardService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DashboardService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public int GetTotalEmployeeCount()
        {
            return _employeeRepository.GetAll().Count;
        }

        public List<Employee> SearchEmployees(string searchName = "", int? departmentId = null)
        {
            var employees = _employeeRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                employees = employees.Where(e => 
                    e.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                    e.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            if (departmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == departmentId.Value).ToList();
            }

            return employees;
        }

        public List<object> GetEmployeesByDepartment()
        {
            var employees = _employeeRepository.GetAll();
            var departments = _departmentRepository.GetAll();

            return departments.Select(d => new
            {
                DepartmentName = d.Name,
                Count = employees.Count(e => e.DepartmentId == d.Id)
            }).ToList<object>();
        }

        public List<object> GetRecentHires(int days = 30)
        {
            var cutoffDate = DateTime.Today.AddDays(-days);
            var employees = _employeeRepository.GetAll();
            var departments = _departmentRepository.GetAll();
            
            return employees
                .Where(e => e.HireDate >= cutoffDate)
                .OrderByDescending(e => e.HireDate)
                .Select(e => new
                {
                    Employee = e,
                    DepartmentName = departments.FirstOrDefault(d => d.Id == e.DepartmentId)?.Name ?? "Unknown"
                })
                .ToList<object>();
        }

        public List<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAll();
        }

        public object GetDashboardData(string searchName = "", int? departmentId = null)
        {
            var filteredEmployees = SearchEmployees(searchName, departmentId);
            
            return new
            {
                TotalEmployees = filteredEmployees.Count,
                EmployeesByDepartment = GetEmployeesByDepartment(),
                RecentHires = GetRecentHires(),
                Departments = GetAllDepartments()
            };
        }
    }
}
