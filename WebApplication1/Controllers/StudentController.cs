using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class StudentController:Controller
    {
        [Authorize(Roles = "Student")]
        public IActionResult StudentMainPage() { return View(); }



    }
}
