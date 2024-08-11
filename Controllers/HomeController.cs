using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Services;
using System.Diagnostics;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuizService _quizService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "quiz.json");
            _quizService = new QuizService(filePath);
        }

        public IActionResult Index()
        {
            var quiz = _quizService.LoadQuiz();
            return View(quiz);
        }

        [HttpPost]
        public IActionResult SubmitQuiz(IFormCollection form)
        {
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

            var quiz = _quizService.LoadQuiz();
            var results = _quizService.EvaluateQuiz(userAnswers, quiz);

            ViewBag.Results = results;

            return View("Index", quiz);
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
