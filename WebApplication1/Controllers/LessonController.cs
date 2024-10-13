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

        public IActionResult AddHomeWork(Guid lessonId)
        {
            var homework = new Homework
            {
                LessonId = lessonId,
                Question = new List<string>(),
                Answer = new List<string>(),
                CorrectAnswer = new List<int>()
            };
            return View(homework);
        }


        [HttpPost]
        public async Task<IActionResult> CreateHomeworkTeacher(List<string> question, List<string> answer, List<int> correctanswer, Guid lessonid)
        {
            var db = new DataBaseContext();
            if (ModelState.IsValid)
            {
                // Проверяем, что количество вопросов, ответов и правильных ответов совпадает
                if (question.Count != answer.Count / 3 ||
                    question.Count != correctanswer.Count)
                {
                    ModelState.AddModelError("", "Неправильное количество данных в форме.");
                    return View("CreateHomeworkTeacher"); // Вернуть представление с ошибкой
                }

                // Проверяем, что правильный ответ выбран для каждого вопроса
                for (int i = 0; i < question.Count; i++)
                {
                    if (correctanswer[i] < 1 || correctanswer[i] > 3)
                    {
                        ModelState.AddModelError("", $"Неверный правильный ответ для вопроса {i + 1}");
                        return View("CreateHomeworkTeacher"); // Вернуть представление с ошибкой
                    }
                }

                // Создание новой домашней работы
                var homework = new Homework
                {
                    LessonId = lessonid,
                    Question = question,
                    Answer = answer,
                    CorrectAnswer = correctanswer
                };

                // Сохранение домашней работы в базу данных
                db.Add(homework);
                await db.SaveChangesAsync();

                // Перенаправляем на страницу с уроками 
                return RedirectToAction("Index", "Home");
            }

            return View("CreateHomeworkTeacher");
        }

        public async Task<IActionResult> DoHomework(int id)
        {
            var db = new DataBaseContext();
            var homework = await db.Homeworks.FindAsync(id);
            return View(homework);
        }


        // id в этом месте это id домашней работы
        public async Task<IActionResult> CheckHomework(int id, List<int> answers)
        {
            var db = new DataBaseContext();
            var homework = await db.Homeworks.FindAsync(id);

            int correctAnswers = 0;

            for (int i = 0; i < homework.Question.Count; i++)
            {
                int correctAnswerIndex = homework.CorrectAnswer[i] - 1;
                if (answers[i] - 1 == correctAnswerIndex)
                {
                    correctAnswers++;
                }
            }

            int valueofhomework = CalculateGrade(correctAnswers);

            var studentid = Request.Cookies["Cookie"];

            var Grade = new ValueOfHomework
            {
                HomeworkId = Convert.ToInt32(id),
                Grade = valueofhomework,
                StudentId = Guid.Parse(studentid),
                Homework = homework,
            };

            db.ValuesOfHomework.Add(Grade);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private int CalculateGrade(int correctAnswersCount)
        {
            if (correctAnswersCount >= 8)
            {
                return 5;
            }
            else if (correctAnswersCount >= 7)
            {
                return 4;
            }
            else if (correctAnswersCount >= 6)
            {
                return 3;
            }
            else if (correctAnswersCount >= 5)
            {
                return 2;
            }
            else
            {
                return 2; // Или "Неудовлетворительно" 
            }
        }

    }
}
