using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Labb3_NET22.Views;

namespace Labb3_NET22.DataModels;   

public class Quiz
{
    private int _id = 0;
    public int Id => _id;
    private IEnumerable<Question> _questions;
    private string _title = string.Empty;
    public IEnumerable<Question> Questions => _questions;
    public string Title => _title;

    public Quiz()
    {
        _questions = new List<Question>();
    }

    public Quiz (int Id, List<Question> questions, string title)
    {
        _id = Id;
        _questions = questions;
        _title = title;
    }


    public Question? GetRandomQuestion(string? category)
    {
        var random = new Random();
        var notAskedQuestions = _questions.ToList()
            .FindAll(q => !q.IsAsked && q.Category.ToString() == category || category == "All" && !q.IsAsked);

        if (notAskedQuestions.Count == 0) 
            return null;

        var result = notAskedQuestions[random.Next(notAskedQuestions.Count)];
        return result;
    }

    public void AddQuestion(int id, string statement, int correctAnswer, string imagePath, Categories category, params string[] answers)
    {
        _questions.ToList().Add(new Question
        {
            Id = id,
            Statement = statement,
            Answers = answers,
            CorrectAnswer = correctAnswer,
            ImagePath = imagePath,
            Category = category,
            IsAsked = false
        });
    }

    public void RemoveQuestion(int index)
    {
        var questions = _questions.ToList();

        if (questions.Count <= index)
            return;

        var temp = questions[index];
        questions.Remove(temp);
    }
}