using System.Collections.Generic;

namespace QuizApp.Models
{
    public class StudyFile
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<VocabularyWord> Vocabulary { get; set; }
    }

    public class VocabularyWord
    {
        public string Term { get; set; }
        public string Definition { get; set; }
    }

}
