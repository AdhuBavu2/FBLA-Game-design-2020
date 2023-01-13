using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement; 

public class QuizManager : MonoBehaviour
{
    public Question[] questions;
    public static List<Question> unansweredQuestions=null;

    private Question currentQuestion;
    public bool answer = true;
    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    public float timeBetweenQuestions = 1f;

    private void Start ()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        SetCurrentQuestion();
        Debug.Log(currentQuestion.fact + "is" + currentQuestion.isTrue);
        if (unansweredQuestions.Count == 6)
        {
            Debug.Log("Count reached");
            if (answer==true)
            { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log("Answer is true ");

            }
             else
            { 
                Debug.Log("You Failed");
             
            }
        }
    }
    private void SetCurrentQuestion ()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;
        
    }

    IEnumerator TransitionToNextQuestion ()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue ()
    {
        //animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT");
            animator.SetTrigger("True");
        }
        else
        {
            Debug.Log("WRONG");
            animator.SetTrigger("False");
            answer = false;
            Debug.Log("Answer set to false 1");
        }
        StartCoroutine(TransitionToNextQuestion());
    } 
    
    public void UserSelectFalse ()
    {
      //  animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("CORRECT");
            animator.SetTrigger("True");
        }
        else
        {
            Debug.Log("WRONG");
            animator.SetTrigger("False");
            answer = false;
            Debug.Log("Answer set to false 2");
        }
        StartCoroutine(TransitionToNextQuestion());
    }

}
