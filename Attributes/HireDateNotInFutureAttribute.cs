using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Attributes
{
    public class HireDateNotInFutureAttribute : ValidationAttribute
    {
        public HireDateNotInFutureAttribute()
        {
            ErrorMessage = "Hire date cannot be in the future.";
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime hireDate)
            {
                return hireDate <= DateTime.Today;
            }
            return true; // Let other validations handle null/invalid types
        }
    }
}
