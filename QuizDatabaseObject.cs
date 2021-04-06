using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz Database", menuName = "Quiz/Database")]
public class QuizDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public QuizObject[] Questions;
    public Dictionary<int, QuizObject> GetQuiz = new Dictionary<int, QuizObject>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Questions.Length; i++)
        {
            Questions[i].Id = i;
            GetQuiz.Add(i, Questions[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetQuiz = new Dictionary<int, QuizObject>();
    }
}
