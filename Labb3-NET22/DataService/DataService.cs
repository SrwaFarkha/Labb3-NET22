using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.DataService
{
    public class DataService
    {
        public List<Quiz> GetAllQuizzes()
        {
            var quizzes = new List<Quiz>(); // skapar en ny lista av quiz

            var quiz1 = new Quiz(
                1,
                new List<Question>
                {
                    new Question(1, "quiz1 Statement1", new[] { "quiz1 answer1", "quiz1 answer2", "quiz1 answer3" }, 1, Categories.Movies,"quiz1 imagePath1"),
                    new Question(2, "quiz1 Statement2", new[] { "quiz1a", "quiz1b" }, 2, Categories.General, "quiz1path2"),
                    new Question(3, "quiz1 Statement3", new[] { "quiz1a", "quiz1b" }, 3, Categories.General, "quiz1path3")
                },
                "quiz1"
            );

            var quiz2 = new Quiz(
                2,
                new List<Question>
                {
                    new Question(1, "quiz2testStatement1", new[] { "quiz2a", "quiz2b" }, 1, Categories.Movies, "quiz2path1" ),
                    new Question(2, "quiz2testStatement2", new[] { "quiz2a", "quiz2b" }, 2, Categories.General, "quiz2path2"),
                    new Question(3, "quiz2testStatement3", new[] { "quiz2a", "quiz2b" }, 3, Categories.Books, "quiz2path3")
                },
                "quiz2"
            );

            var quiz3 = new Quiz(
                3,
                new List<Question>
                {
                    new Question(1, "quiz3testStatement1", new[] { "quiz3a", "quiz3b" }, 1,Categories.General, "quiz3path1"),
                    new Question(2, "quiz3testStatement2", new[] { "quiz3a", "quiz3b" }, 2,  Categories.Books, "quiz3path2"),
                    new Question(3, "quiz3testStatement3", new[] { "quiz3a", "quiz3b" }, 3, Categories.Books, "quiz3path3")
                },
                "quiz3"
            );

            quizzes.Add(quiz1);
            quizzes.Add(quiz2);
            quizzes.Add(quiz3);

            return quizzes;
        }
    }
}
