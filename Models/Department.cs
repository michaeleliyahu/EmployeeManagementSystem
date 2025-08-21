using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<Employee> Employees { get; set; } = new();
    }
}
