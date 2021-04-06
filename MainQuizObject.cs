using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Quiz", menuName = "Quiz/Quiz")]

public class MainQuizObject : ScriptableObject
{
    public QuizDatabaseObject database;
    public Questions Container;
    
[System.Serializable]
public class Questions
{
    public QuestionsPool[] Items = new QuestionsPool[10];
}

[System.Serializable]
public class QuestionsPool
{
    public int ID = -1;
    public Question question;
    public int amount;
    public QuestionsPool()
    {
        ID = -1;
        question = null;
        amount = 0;
    }
    public QuestionsPool(int _id, Question _question, int _amount)
    {
        ID = _id;
        question = _question;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Question _question, int _amount)
    {
        ID = _id;
        question = _question;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
}
