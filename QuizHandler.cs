using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class QuizHandler : MonoBehaviour
{
    public QuizDatabaseObject quizDatabase;
    List<QuizObject> questionPool = new List<QuizObject>();
    public TextMeshProUGUI question;
    public Image image;
    public Button[] answers;
    private float hasWaited;
    private float coolDown = 1.5f;
    List<string> ansSelected = new List<string>();
    QuizObject currentQuestion;
    public TextMeshProUGUI num;
    private int numCount = 0;
    public TextMeshProUGUI ansText;
    public GameObject wrong;
    public GameObject correct;

    void Start()
    {
        PopulateArray();
        GetQuestion();
    }

    private void LoadAnswers()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI ans = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            answers[i].onClick.AddListener(delegate {TaskOnClick(ans); });
        }
    }

    public void ClickAns(TextMeshProUGUI ans)
    {
        if(Time.time < hasWaited)
            return;
        hasWaited = Time.time + coolDown;
        if(ansSelected.Contains(ans.text))
        {
            ans.color = new Color32(0,0,0,255);
            ansSelected.Remove(ans.text);
        }
        else
        {
            ansSelected.Add(ans.text);
            ans.color = new Color32(0,0,255,255);
        }
    }

    public void CheckAns()
    {
        List<string> questionList = new List<string>();
            for (int i = 0; i < currentQuestion.correctAnswers.Length; i++)
            {
                questionList.Add(currentQuestion.correctAnswers[i]);
            }
        if(currentQuestion.correctAnswers.Length == 1)
        {
            if(ansSelected.Count > 1)
            {
                Debug.Log("Too many answers selected. Choose only one.");
                return;
            }
            else 
            {
                if(currentQuestion.correctAnswers[0] == ansSelected[0])
                {
                    correct.SetActive(true);
                    ansText.text = currentQuestion.correctAnswers[0];
                    Debug.Log("Correct." + currentQuestion.correctAnswers[0]);
                }
                else
                {
                    wrong.SetActive(true);
                    ansText.text = currentQuestion.correctAnswers[0];
                    Debug.Log("Wrong." + currentQuestion.correctAnswers[0]);
                }
            }
        }
        else
        {
            if(CheckLists(questionList, ansSelected))
            {
                correct.SetActive(true);
                for (int i = 0; i < questionList.Count; i++)
                {
                    ansText.text = ansText.text + " " + questionList[i];
                    Debug.Log("Answer is Correct" + questionList[i]);
                }
            }
            else {
                wrong.SetActive(true);
                for (int i = 0; i < questionList.Count; i++)
                {
                    ansText.text = ansText.text + " " + questionList[i];
                    Debug.Log("Answer is Wrong" + questionList[i]);
                }
            }
        }
    }

    public static bool CheckLists<T>(List<T> listOne, List<T> listTwo)
    {
        if (listOne == null || listTwo == null || listOne.Count != listTwo.Count) {
            return false;
        }
        if (listOne.Count == 0) {
            return true;
        }
        Dictionary<T, int> searchList = new Dictionary<T, int>();
        
        for(int i = 0; i < listOne.Count; i++) {
            int count = 0;
            if (!searchList.TryGetValue(listOne[i], out count)) {
                searchList.Add(listOne[i], 1);
                continue;
            }
            searchList[listOne[i]] = count + 1;
        }
        for (int i = 0; i < listTwo.Count; i++) {
            int count = 0;
            if (!searchList.TryGetValue(listTwo[i], out count)) {
                return false;
            }
            count--;
            if (count <= 0) {
                searchList.Remove(listTwo[i]);
            }
            else {
                searchList[listTwo[i]] = count;
            }
        }
        return searchList.Count == 0;
}

    private void TaskOnClick(TextMeshProUGUI ans)
    {
        ClickAns(ans);
    }

    public void GetQuestion()
    {
        numCount = numCount + 1;
        num.text = numCount.ToString();
        if(questionPool.Count == 0)
        {
            PopulateArray();
            numCount = 1;
            num.text = numCount.ToString();
        }
        wrong.SetActive(false);
        correct.SetActive(false);
        ansText.text = "";
        ansSelected.Clear();
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0,0,0,255);
        }
        
        ClearQuestion();
        currentQuestion = questionPool[0];
        question.text = questionPool[0].question;
        if(questionPool[0].quizImg != null)
        {
            image.sprite = questionPool[0].quizImg;
        }
        List<string> answersRandom = new List<string>();
        for (int i = 0; i < questionPool[0].answers.Length; i++)
        {
            answersRandom.Add(questionPool[0].answers[i]);
        }
        ShuffleAns(answersRandom);
        for (int i = 0; i < answersRandom.Count; i++)
        {
            answers[i].GetComponentInChildren<TextMeshProUGUI>().text = answersRandom[i];
        }
        LoadAnswers();
        questionPool.RemoveAt(0);
    }

    public void PopulateArray()
    {
        for(int i = 0; i < quizDatabase.Questions.Length; i++) 
        {
            questionPool.Add(quizDatabase.Questions[i]);
        }
        Shuffle(questionPool);
        Debug.Log(questionPool[0].question);
    }
    
    public static List<QuizObject> Shuffle (List<QuizObject>aList) {
 
        System.Random _random = new System.Random ();
 
        QuizObject quizObj;
 
        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));
            quizObj = aList[r];
            aList[r] = aList[i];
            aList[i] = quizObj;
        }
 
        return aList;
    }

    public static List<string> ShuffleAns (List<string>aList) {
 
        System.Random _random = new System.Random ();
 
        string quizObj;
 
        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));
            quizObj = aList[r];
            aList[r] = aList[i];
            aList[i] = quizObj;
        }
 
        return aList;
    }

    public void ClearQuestion()
    {
        question.text = "";
        image.sprite = null;
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
}
