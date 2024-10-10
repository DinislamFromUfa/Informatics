using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.ViewModels;
using System.Diagnostics.Eventing.Reader;


namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {
        public async Task<IActionResult> MyCourses() {
            var db = new DataBaseContext();
            if (User.IsInRole("Student"))
            {
                var StudentId = Request.Cookies["Cookie"];
                var student = await db.Students
                    .Include(s => s.Courses)
                    .ThenInclude(c => c.Lessons)
                    .FirstOrDefaultAsync(s => s.Id == Guid.Parse(StudentId));
                return View(new UserViewModel
                {
                    Student = student,
                });
            }

            var TeacherId = Request.Cookies["Cookie"];
            var teacher = await db.Teachers
                .Include(t => t.Courses)
                .ThenInclude(c => c.Lessons)
                .FirstOrDefaultAsync(t => t.ID == Guid.Parse(TeacherId));
            return View(new UserViewModel
            {
                Teacher = teacher,
            });
        }

        public IActionResult AddNewCourse() { return View(); }

        public IActionResult CreateCourse()
        {
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AllCourses(Course course) {

            var db = new DataBaseContext();
            var listCourses = await db.Courses.ToListAsync();
            return View(listCourses);
        }



        public async Task<IActionResult> CreateNewCourse(Guid id, string coursename, string description)
        {
            var CookieIdTeacher = Request.Cookies["Cookie"];
            var NewCourse = new Course
            {
                Id = id,
                Description = description,
                TeacherId = Guid.Parse(CookieIdTeacher),
                CourseName = coursename,
            };
            var db = new DataBaseContext();
            await db.AddRangeAsync(NewCourse);
            await db.SaveChangesAsync();
            return RedirectToAction("MyCourses");
        }

        public async Task<IActionResult> Details(Guid id){
            var db = new DataBaseContext();
            var course = await db.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        public async Task<IActionResult> EditCourse() { 
            var db = new DataBaseContext();
            var CookieIdTeacher = Request.Cookies["Cookie"];
            var courses = await db.Courses.Where(c => c.TeacherId == Guid.Parse(CookieIdTeacher)).ToListAsync();
            return View(courses);
        }

        public IActionResult ReviewOnCourse() { return View(); }

        public async Task<IActionResult> PersonalAccount()
        { 
            var db = new DataBaseContext();
            if (User.IsInRole("Student"))
            {
                var studentid = Request.Cookies["Cookie"];
                var student = await db.Students.FirstOrDefaultAsync(s => s.Id == Guid.Parse(studentid));
                var viewmodel = new UserViewModel
                {
                    Student = student,
                };
                return View(viewmodel);
            }
            else if (User.IsInRole("Teacher"))
            {
                var teacherid = Request.Cookies["Cookie"];
                var teacher = await db.Teachers.FirstOrDefaultAsync(t => t.ID == Guid.Parse(teacherid));
                var viewmodel = new UserViewModel
                {
                    Teacher = teacher,
                };
                return View(viewmodel);
            }
            return NotFound();
        }



        public IActionResult AddHomeWork() { return View(); }



        public async Task<IActionResult> EditDetails(Guid courseId)
        {
            var db = new DataBaseContext();
            var course = await db.Courses.Include(c => c.Lessons).FirstOrDefaultAsync(course => course.Id == courseId);
            if (course == null) { return NotFound(); }
            return View(course);
        }

        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            var db = new DataBaseContext();
            var course = await db.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null) { return NotFound(); };
            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction("EditCourse", "Courses");

        }

        public IActionResult Create(Guid courseId)
        {
            var db = new DataBaseContext();
            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null) { return NotFound(); }

            return View(course);
        }

        public async Task<IActionResult> SubscribeCourse(Guid courseId)
        {
            var db = new DataBaseContext();
            var studentId = Request.Cookies["Cookie"];
            var student = await db.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.Id == Guid.Parse(studentId));
            var course = await db.Courses.FindAsync(courseId);

            if (student == null || course == null)
            {
                return NotFound();
            }

            if (student.Courses.Any(c => c.Id == courseId))
            {
                TempData["Message"] = "Студент уже подписан на этот курс.";
                return RedirectToAction("MyCourses");
            }

            student.Courses.Add(course);
            await db.SaveChangesAsync();

            TempData["Message"] = "Студент успешно подписан на курс.";
            return RedirectToAction("MyCourses");
        }

        public async Task<IActionResult> UpdateDetails(Guid courseId, string nameofcourse, string description) {
            var db = new DataBaseContext();
            await db.Courses
                .Where(c => c.Id == courseId)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.CourseName, nameofcourse)
                .SetProperty(c => c.Description, description));
            await db.SaveChangesAsync();
            return RedirectToAction("EditCourse");
        }
    }
}
