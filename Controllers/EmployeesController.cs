using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;

        public EmployeesController(EmployeeService employeeService, DepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        // GET: Employees
        public IActionResult Index(int pageNumber = 1, string sortBy = "FirstName")
        {
            const int pageSize = 10;

            try
            {
                var employees = _employeeService.GetAllWithPaging(pageNumber, sortBy, pageSize);
                var totalEmployees = _employeeService.GetTotalCount();
                var totalPages = (int)Math.Ceiling(totalEmployees / (double)pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = totalPages;
                ViewBag.SortBy = sortBy;
                ViewBag.TotalEmployees = totalEmployees;

                return View(employees);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Employee>());
            }
        }

        // GET: Employees/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var employee = _employeeService.GetById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                // Get department name
                var department = _departmentService.GetById(employee.DepartmentId);
                employee.Department = department;

                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.Departments = _departmentService.GetAll();
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Create(employee);
                    TempData["Success"] = "Employee created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.Departments = _departmentService.GetAll();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var employee = _employeeService.GetById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                ViewBag.Departments = _departmentService.GetAll();
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Update(employee);
                    TempData["Success"] = "Employee updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.Departments = _departmentService.GetAll();
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = _employeeService.GetById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                // Get department name
                var department = _departmentService.GetById(employee.DepartmentId);
                employee.Department = department;

                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _employeeService.Delete(id);
                TempData["Success"] = "Employee deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
