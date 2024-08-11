namespace QuizApp.Models
{
    public class Question
    {
        public int Id { get; set; } // The Id property for the question
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOptionIndex { get; set; }
    }
}