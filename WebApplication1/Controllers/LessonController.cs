using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult CreateNewLesson(Guid courseId)
        {
            var lesson = new Lesson {CourseId = courseId };
            return View(lesson);
        }

        public async Task<IActionResult> AddFiles(string name, IFormFileCollection contentfiles, Guid courseId)
        {
            var db = new DataBaseContext();
            var lesson = new Lesson
            {
                Name = name,
                CourseId = courseId,
                ContentFiles = new List<string>() // Инициализируем список
            };

            // Получаем путь к рабочему столу
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Создаем папку "Файлы курсов" на рабочем столе, если она еще не существует
            string courseFilesPath = Path.Combine(desktopPath, "Файлы курсов");
            Directory.CreateDirectory(courseFilesPath);

            // Получаем курс из базы данных
            var course = await db.Courses.FindAsync(courseId);

            // Проверяем, что курс найден
            if (course != null)
            {
                // Создаем папку для курса, если она еще не существует
                string courseFolderPath = Path.Combine(courseFilesPath, course.CourseName);
                Directory.CreateDirectory(courseFolderPath);

                // Создаем папку "Уроки" внутри папки курса, если она еще не существует
                string lessonsFolderPath = Path.Combine(courseFolderPath, "Уроки");
                Directory.CreateDirectory(lessonsFolderPath);

                // Сохраняем файлы
                if (contentfiles.Count > 0)
                {
                    foreach (var file in contentfiles)
                    {
                        // Получаем имя файла
                        string fileName = Path.GetFileName(file.FileName);

                        // Сохраняем файл в папку "Уроки"
                        string filePath = Path.Combine(lessonsFolderPath, fileName);
                        using (var stream = file.OpenReadStream())
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await stream.CopyToAsync(fileStream);
                            }
                        }

                        // Добавляем путь к файлу в список
                        lesson.ContentFiles.Add(filePath);
                    }
                }

                await db.AddAsync(lesson);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Выводим сообщение об ошибке пользователю
                ModelState.AddModelError("", "Курс не найден.");
                return View(lesson);
            }
        }

        public IActionResult DownloadFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var fileName = Path.GetFileName(filePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            return NotFound();
        }

        public IActionResult UpdateLesson()
        {
            return View();
        }

        public async Task<IActionResult> DeleteLesson(Guid lessonId)
        {
            var db = new DataBaseContext();
            Lesson? lesson = await db.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
            db.Lessons.Remove(lesson);
            await db.SaveChangesAsync();
            return RedirectToAction("EditCourse", "Courses");
        }
    }
}
