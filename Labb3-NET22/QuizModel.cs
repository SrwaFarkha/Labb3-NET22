using Labb3_NET22.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_NET22
{
    public class QuizModel
    {
        public int Id { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public string Title { get; set; } = "";
    }
}
