using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department? GetById(int id);
        void Add(Department department);
        void Update(Department department);
        void Delete(int id);
        bool HasEmployees(int departmentId);
    }
}
