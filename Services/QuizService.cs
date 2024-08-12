using System.IO;
using System.Text.Json;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class QuizService
    {
        public Quiz LoadQuiz(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            var quiz = JsonSerializer.Deserialize<Quiz>(jsonString);

            if (quiz == null)
            {
                throw new InvalidOperationException("Failed to load quiz from JSON file.");
            }

            return quiz;
        }

        public Dictionary<int, bool> EvaluateQuiz(Dictionary<int, int> userAnswers, Quiz quiz)
        {
            var results = new Dictionary<int, bool>();

            foreach (var question in quiz.Questions)
            {
                if (userAnswers.TryGetValue(question.Id, out int selectedOption))
                {
                    results[question.Id] = (selectedOption == question.CorrectOptionIndex);
                }
            }

            return results;
        }
    }
}
