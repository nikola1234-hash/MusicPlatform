using Microsoft.AspNetCore.Mvc;
using MusicPlatform.Models.QuizModels;
using MusicPlatform.Services.Quiz;
using System.Text;

namespace MusicPlatform.Controllers
{
    public class QuizController : Controller
    {

        private readonly ILogger<QuizController> _logger;
        private readonly IQuizService _quizService;

        public QuizController(ILogger<QuizController> logger, IQuizService quizService)
        {
            _logger = logger;
            _quizService = quizService;
        }

        public async Task<IActionResult> Index()
        {

            QuizViewModel model = new QuizViewModel();
            model.Questions = await _quizService.GenerateQuestions();

            foreach(var question in model.Questions)
            {
                var bytes = Encoding.UTF8.GetBytes(question.CorrectAnswer);
                question.CorrectAnswer = Convert.ToBase64String(bytes);

            }
            return View(model);
        }
    }
}
