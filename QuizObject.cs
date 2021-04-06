using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuizType
{
    Quiz,
    Default
}

public class QuizObject : ScriptableObject
{
    public int Id;
    public QuizType type;
    [TextArea(15,20)]
    public string question;
    public string[] answers;
    public string[] correctAnswers;
    public Sprite quizImg;

    public Question CreateQuestion()
    {
        Question newQuestion = new Question(this);
        return newQuestion;
    }
}

[System.Serializable]
public class Question
{
    public int Id;
    public QuizType Type;
    public string QuestionAsked;
    public string[] Answers;
    public string[] CorrectAnswers;
    public Sprite Img;
    public Question(QuizObject question)
    {
        Id = question.Id;
        Type = question.type;
        QuestionAsked = question.question;
        Answers = question.answers;
        CorrectAnswers = question.correctAnswers;
        Img = question.quizImg;
    }
}