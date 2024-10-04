using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
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


        public IActionResult LoginOnSite() { 
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProfile(Student student)
        {
            using (var context = new DataBaseContext())
            {
                student.Role = "Student";
                context.Students.Add(student);
                context.SaveChanges();
            }
            return RedirectToAction("LoginOnSite");
        }

        public IActionResult Teacher()
        {
            return View();
        }

        public IActionResult RegistrationForTeacher() { return View(); }

        public IActionResult CreateProfileTeacher(Teacher teacher)
        {
            using (var context = new DataBaseContext())
            {
                teacher.Role = "Teacher";
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
            return RedirectToAction("LoginOnSite");
        }


        public async Task <IActionResult> CheckingOnSite(Student student)
        {
            var db = new DataBaseContext();
            // находим пользователя
            var user = db.Students.FirstOrDefault(u => u.Username == student.Username && u.Password == student.Password);
            // если название пользователя и/или пароль не установлены, посылаем статусный код ошибки 400
            if (user is null)
            {
                return RedirectToAction("Error");
            } 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal);
            return RedirectToAction("StudentMainpage", "Student");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
