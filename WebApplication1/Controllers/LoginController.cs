using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Permissions;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult LoginOnSite() { return View(); }


        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProfile(Student student)
        {
            using (var context = new DataBaseContext())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            return RedirectToAction("LoginOnSite");
        }


        public IActionResult AdminPanel()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CheckingOnSite(string username, string password)
        {

            if (username == "admin" && password == "admin") {
                return RedirectToAction("AdminPanel");
            }

            using (var db = new DataBaseContext())
            {
                var student = db.Students.SingleOrDefault(s => s.Username == username && s.Password == password);
                if (student != null)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильное имя пользователя или пароль.");
                    return View();
                }
            }
        }

        public IActionResult Teacher()
        {
            return View();
        }

    }
}
