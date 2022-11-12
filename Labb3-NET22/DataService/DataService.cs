using Labb3_NET22.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_NET22.DataService
{
    public class DataService
    {
        public async Task<List<Quiz>> GetDefaultQuizzes()
        {
            return await Task.FromResult(new List<Quiz>()
            {
                new Quiz(
                    1,
                    new List<Question>
                    {
                        new Question(1, "What is 1 + 1?", new[] { "A. 2", "B. 3","C. 5" }, 1, "cat1.jpg", Categories.Addition),
                        new Question(2, "What is 1 + 2?", new[] { "A. 4", "B. 3","C. 5" }, 2, "cat2.jpg", Categories.Addition),
                        new Question(3, "What is 1 + 3?", new[] { "A. 4", "B. 3","C. 7" }, 1, "cat3.jpg", Categories.Addition),
                        new Question(4, "What is 1 + 4?", new[] { "A. 5", "B. 3","C. 7" }, 1, "cat4.jpg", Categories.Addition),
                        new Question(5, "What is 1 + 5?", new[] { "A. 4", "B. 3","C. 6" }, 3, "cat5.jpg", Categories.Addition),

                        new Question(6, "What is 1 - 1?", new[] { "A. 2", "B. 1","C. 0" }, 3, "cat6.jpg", Categories.Subtraction),
                        new Question(7, "What is 15 - 5?", new[] { "A. 9", "B. 10","C. 7" }, 2, "cat7.jpg", Categories.Subtraction),
                        new Question(8, "What is 10 - 3?", new[] { "A. 5", "B. 6","C. 7" }, 3, "cat8.jpg", Categories.Subtraction),
                        new Question(9, "What is 6 - 3?", new[] { "A. 5", "B. 3","C. 7" }, 2, "cat9.jpg", Categories.Subtraction),
                        new Question(10, "What is 5 - 2?", new[] { "A. 4", "B. 3","C. 6" }, 2, "cat10.jpg", Categories.Subtraction),

                        new Question(11, "What is 2 x 2?", new[] { "A. 2", "B. 4","C. 8" }, 2, "cat11.jpg", Categories.General),
                        new Question(12, "What is 3 x 2?", new[] { "A. 4", "B. 3","C. 6" }, 3, "cat12.jpg", Categories.General),
                        new Question(13, "What is 6 x 3?", new[] { "A. 7", "B. 8","C. 9" }, 3, "cat13.jpg", Categories.General),
                        new Question(14, "What is 9 x 4?", new[] { "A. 53", "B. 36","C. 47" }, 2, "cat14.jpg", Categories.General),
                        new Question(15, "What is 1 x 5?", new[] { "A. 4", "B. 3","C. 5" }, 3, "cat15.jpg", Categories.General),

                        new Question(16, "What is 20 / 4?", new[] { "A. 2", "B. 3","C. 5" }, 3, "cat16.jpg", Categories.General),
                        new Question(17, "What is 80 / 2?", new[] { "A. 40", "B. 30","C. 50" }, 1, "cat17.jpg", Categories.General),
                        new Question(18, "What is 60 / 3?", new[] { "A. 40", "B. 30","C. 20" }, 3, "cat18.jpg", Categories.General),
                        new Question(19, "What is 8 / 4?", new[] { "A. 4", "B. 3","C. 2" }, 3, "cat19.jpg", Categories.General),
                        new Question(20, "What is 10 / 5?", new[] { "A. 4", "B. 3","C. 5" }, 3, "cat20.jpg", Categories.General),

                     },
                    "quiz1"
                )
            });
        }
    }
}
