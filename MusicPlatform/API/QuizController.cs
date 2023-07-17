using Microsoft.AspNetCore.Mvc;
using MusicPlatform.DTO;
using System.Text;

namespace MusicPlatform.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        [HttpPost("submit")]
        public IActionResult GetResult(QuizModel quizQuestions)
        {
            int score = 0;
            foreach(var question in quizQuestions.QuizQuestions)
            {
                foreach(var answer in quizQuestions.Answers)
                {
                    if(answer.Id == question.Id)
                    {
                      
                        var correct = Convert.FromBase64String(question.CorrectAnswer);
                        var correctAnswer = Encoding.UTF8.GetString(correct);
                        if(answer.Answer == correctAnswer)
                        {
                            score += 10;
                        }
                       
                    }
                }
             
            }

            return Ok(score);
        }
    }
}
