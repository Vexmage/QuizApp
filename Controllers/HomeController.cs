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

        // Inject the logger and QuizService into the controller
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "quiz.json");
            _quizService = new QuizService(filePath);
        }

        public IActionResult Index()
        {
            // Load the quiz using the service
            var quiz = _quizService.LoadQuiz();
            return View(quiz);
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
