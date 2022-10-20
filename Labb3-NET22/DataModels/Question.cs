namespace Labb3_NET22.DataModels;

public class Question
{

    public int Id { get; set; }
    public string Statement { get; }
    public string[] Answers { get; }
    public int CorrectAnswer { get; }
    public Categories Categories { get; set; }
    public string ImagePath { get; set; }

    public Question(int id, string statement, string[] answers, int correctAnswer, Categories categories, string imagePath)
    {
        Id = id;
        Statement = statement;
        Answers = answers;
        CorrectAnswer = correctAnswer;
        Categories = categories;
        ImagePath = imagePath;
    }


}