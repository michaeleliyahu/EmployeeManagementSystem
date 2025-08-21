using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;

namespace EmployeeManagementSystem.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public List<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department? GetById(int id)
        {
            return _departmentRepository.GetById(id);
        }

        public void Create(Department department)
        {
            // Business validation
            ValidateDepartment(department);

            // Check for duplicate name
            if (GetAll().Any(d => d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Department name already exists.");
            }

            _departmentRepository.Add(department);
        }

        public void Update(Department department)
        {
            // Business validation
            ValidateDepartment(department);

            // Check if department exists
            if (_departmentRepository.GetById(department.Id) == null)
            {
                throw new ArgumentException("Department not found.");
            }

            // Check for duplicate name (excluding current department)
            if (GetAll().Any(d => d.Id != department.Id && 
                                 d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Department name already exists.");
            }

            _departmentRepository.Update(department);
        }

        public void Delete(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
            {
                throw new ArgumentException("Department not found.");
            }

            // Business rule: Cannot delete department with employees
            if (_departmentRepository.HasEmployees(id))
            {
                throw new InvalidOperationException("Cannot delete department with existing employees. Please reassign or remove employees first.");
            }

            _departmentRepository.Delete(id);
        }

        public bool CanDelete(int id)
        {
            return !_departmentRepository.HasEmployees(id);
        }

        public int GetEmployeeCount(int departmentId)
        {
            // This would require access to employee repository, but we keep it simple
            return _departmentRepository.HasEmployees(departmentId) ? 1 : 0;
        }

        private void ValidateDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.Name))
                throw new ArgumentException("Department name is required.");

            if (department.Name.Length > 100)
                throw new ArgumentException("Department name cannot exceed 100 characters.");

            if (department.Name.Length < 2)
                throw new ArgumentException("Department name must be at least 2 characters long.");
        }
    }
}
