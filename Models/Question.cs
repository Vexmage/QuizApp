namespace QuizApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOptionIndex { get; set; }
        public string? Explanation { get; set; } // Add the Explanation property
    }

}