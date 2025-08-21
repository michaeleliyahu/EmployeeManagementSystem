using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: Departments
        public IActionResult Index()
        {
            try
            {
                var departments = _departmentService.GetAll();
                return View(departments);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Department>());
            }
        }

        // GET: Departments/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var department = _departmentService.GetById(id);
                if (department == null)
                {
                    return NotFound();
                }

                return View(department);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Create(department);
                    TempData["Success"] = "Department created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var department = _departmentService.GetById(id);
                if (department == null)
                {
                    return NotFound();
                }

                return View(department);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Update(department);
                    TempData["Success"] = "Department updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(department);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                var department = _departmentService.GetById(id);
                if (department == null)
                {
                    return NotFound();
                }

                // Check if department can be deleted
                ViewBag.CanDelete = _departmentService.CanDelete(id);

                return View(department);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _departmentService.Delete(id);
                TempData["Success"] = "Department deleted successfully.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while deleting the department.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
