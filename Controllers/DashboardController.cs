using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: Dashboard
        public IActionResult Index(string searchName = "", int? searchDeptId = null)
        {
            try
            {
                var dashboardData = _dashboardService.GetDashboardData(searchName, searchDeptId);
                
                // Get filtered employees for the current search
                var filteredEmployees = _dashboardService.SearchEmployees(searchName, searchDeptId);
                
                ViewBag.TotalEmployees = filteredEmployees.Count;
                ViewBag.GroupedByDept = _dashboardService.GetEmployeesByDepartment();
                ViewBag.RecentHires = _dashboardService.GetRecentHires();
                ViewBag.Departments = _dashboardService.GetAllDepartments();
                ViewBag.SearchName = searchName;
                ViewBag.SearchDeptId = searchDeptId;

                return View(filteredEmployees);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                
                // Return empty dashboard in case of error
                ViewBag.TotalEmployees = 0;
                ViewBag.GroupedByDept = new List<object>();
                ViewBag.RecentHires = new List<object>();
                ViewBag.Departments = new List<object>();
                ViewBag.SearchName = "";
                ViewBag.SearchDeptId = null;

                return View(new List<EmployeeManagementSystem.Models.Employee>());
            }
        }
    }
}
