using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Services;
using System.Diagnostics;
using System.Text.Json;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuizService _quizService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _quizService = new QuizService();
        }

        public IActionResult Landing()
        {
            return View();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Landing");
        }

        [Authorize]
        public IActionResult SelectQuiz()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadQuiz(string quizFileName, bool timerEnabled = true, int timerDuration = 180)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", quizFileName);
            var quiz = _quizService.LoadQuiz(filePath);

            ViewBag.TimerEnabled = timerEnabled;
            ViewBag.TimerDuration = timerDuration;

            ViewData["QuizFileName"] = quizFileName;
            return View("Quiz", quiz);
        }

        [HttpPost]
        public IActionResult SubmitQuiz(IFormCollection form, string quizFileName)
        {
            if (string.IsNullOrEmpty(quizFileName))
            {
                return RedirectToAction("Index"); // Redirect to quiz selection if quizFileName is missing
            }

            var userAnswers = new Dictionary<int, int>();

            foreach (var key in form.Keys)
            {
                if (key.StartsWith("question_"))
                {
                    var questionId = int.Parse(key.Split('_')[1]);
                    var selectedOption = int.Parse(form[key]);
                    userAnswers.Add(questionId, selectedOption);
                }
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", quizFileName);
            var quiz = _quizService.LoadQuiz(filePath);
            var results = _quizService.EvaluateQuiz(userAnswers, quiz);

            ViewBag.Results = results;
            ViewData["QuizFileName"] = quizFileName;

            return View("Quiz", quiz);
        }

        public IActionResult StudyFiles()
        {
            var studyFiles = LoadAllStudyFiles(); // This loads all study files
            return View(studyFiles);
        }

        private List<StudyFile> LoadAllStudyFiles()
        {
            var studyFiles = new List<StudyFile>();
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "studyFiles");

            foreach (var file in Directory.GetFiles(directoryPath, "*.json"))
            {
                try
                {
                    var jsonString = System.IO.File.ReadAllText(file);
                    var fileStudyFiles = JsonSerializer.Deserialize<List<StudyFile>>(jsonString);

                    if (fileStudyFiles != null)
                    {
                        studyFiles.AddRange(fileStudyFiles);
                    }
                }
                catch (JsonException ex)
                {
                    Debug.WriteLine($"Failed to deserialize file: {file}. Error: {ex.Message}");
                }
            }

            return studyFiles;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
