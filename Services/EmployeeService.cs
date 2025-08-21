using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public Employee? GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public List<Employee> GetAllWithPaging(int pageNumber, string sortBy, int pageSize = 10)
        {
            var employees = GetAll();

            // Sorting
            employees = sortBy switch
            {
                "LastName" => employees.OrderBy(e => e.LastName).ToList(),
                "Email" => employees.OrderBy(e => e.Email).ToList(),
                "HireDate" => employees.OrderBy(e => e.HireDate).ToList(),
                "Salary" => employees.OrderBy(e => e.Salary).ToList(),
                _ => employees.OrderBy(e => e.FirstName).ToList(),
            };

            // Paging
            return employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalCount()
        {
            return GetAll().Count;
        }

        public void Create(Employee employee)
        {
            // Business validation
            ValidateEmployee(employee);

            // Check if department exists
            if (_departmentRepository.GetById(employee.DepartmentId) == null)
            {
                throw new ArgumentException("Department does not exist.");
            }

            _employeeRepository.Add(employee);
        }

        public void Update(Employee employee)
        {
            // Business validation
            ValidateEmployee(employee);

            // Check if employee exists
            if (_employeeRepository.GetById(employee.Id) == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            // Check if department exists
            if (_departmentRepository.GetById(employee.DepartmentId) == null)
            {
                throw new ArgumentException("Department does not exist.");
            }

            _employeeRepository.Update(employee);
        }

        public void Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            _employeeRepository.Delete(id);
        }

        public List<Employee> SearchByName(string searchName)
        {
            if (string.IsNullOrWhiteSpace(searchName))
                return GetAll();

            return GetAll().Where(e =>
                e.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                e.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public List<Employee> GetByDepartment(int departmentId)
        {
            return GetAll().Where(e => e.DepartmentId == departmentId).ToList();
        }

        public List<Employee> GetRecentHires(int days = 30)
        {
            var cutoffDate = DateTime.Today.AddDays(-days);
            return GetAll()
                .Where(e => e.HireDate >= cutoffDate)
                .OrderByDescending(e => e.HireDate)
                .ToList();
        }

        private void ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FirstName))
                throw new ArgumentException("First Name is required.");

            if (string.IsNullOrWhiteSpace(employee.LastName))
                throw new ArgumentException("Last Name is required.");

            if (string.IsNullOrWhiteSpace(employee.Email))
                throw new ArgumentException("Email is required.");

            if (!IsValidEmail(employee.Email))
                throw new ArgumentException("Invalid email format.");

            if (employee.HireDate > DateTime.Today)
                throw new ArgumentException("Hire date cannot be in the future.");

            if (employee.Salary <= 0)
                throw new ArgumentException("Salary must be greater than 0.");

            if (employee.DepartmentId <= 0)
                throw new ArgumentException("Department must be selected.");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
