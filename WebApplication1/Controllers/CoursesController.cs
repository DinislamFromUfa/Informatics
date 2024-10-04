using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult MyCourses() { return View(); }

        public IActionResult AddCourse() { return View(); }

        public IActionResult CreateCourse()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
