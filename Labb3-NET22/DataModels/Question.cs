namespace Labb3_NET22.DataModels;

public class Question
{
    public int Id { get; set; }
    public string Statement { get; set; } = null!;
    public string[] Answers { get; set; } = null!;
    public int CorrectAnswer { get; set; }
    public string ImagePath { get; set; } = null!;
    public Categories Category { get; set; }
    public bool IsAsked { get; set; }

    public Question()
    {
        
    }

    public Question(int id, string statement, string[] answers, int correctAnswer, string imagePath, Categories category)
    {
        Id = id;
        Statement = statement;
        Answers = answers;
        CorrectAnswer = correctAnswer;
        ImagePath = imagePath;
        Category = category;
    }
}