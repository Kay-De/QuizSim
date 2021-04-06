using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz Object", menuName = "Quiz/Quizzes/Default")]

public class DefaultObject : QuizObject
{
    public void Awake()
    {
        type = QuizType.Default;
    }
}
