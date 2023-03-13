using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Question> questions;
    private Question currentQuestion;
    private int currentQuestionIndex;

    [SerializeField]
    private Text questionText;
    [SerializeField]
    private List<Text> optionTexts;
    private int selectionsRemaining;

    [SerializeField]
    private ProgressBar progressBar;
    [SerializeField]
    private float slideDuration;
    [SerializeField]
    private float timeBetweenQuestions;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.SetMaxValue(questions.Count);
        HelperFunctions.Shuffle<Question>(questions);

        LoadQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            currentQuestion = questions[currentQuestionIndex];
            currentQuestionIndex++;

            questionText.text = currentQuestion.question;
            selectionsRemaining = currentQuestion.numberOfCorrectAnswers;

            HelperFunctions.Shuffle<Text>(optionTexts);
            HelperFunctions.Shuffle<string>(currentQuestion.correctAnswers);
            HelperFunctions.Shuffle<string>(currentQuestion.wrongAnswers);

            for (int i = 0; i < optionTexts.Count; i++)
            {
                if (i < currentQuestion.numberOfCorrectAnswers)
                {
                    optionTexts[i].text = currentQuestion.correctAnswers[i];
                } else
                {
                    optionTexts[i].text = currentQuestion.wrongAnswers[i - currentQuestion.numberOfCorrectAnswers];
                }
            }
        } else
        {
            Debug.Log("FINISHED!");
        }
    }

    public void SelectAnswer(Text answerText)
    {
        selectionsRemaining--;

        Animator animator = answerText.GetComponentInParent<Animator>();

        if (currentQuestion.correctAnswers.Contains(answerText.text))
        {
            answerText.text = "CORRECT!";
            animator.SetTrigger("Correct");
        }
        else
        {
            answerText.text = "WRONG!";
            animator.SetTrigger("Wrong");
        }

        if (selectionsRemaining <= 0)
        {
            StartCoroutine(TransitionToNextQuestion());
        } else
        {
            answerText.GetComponentInParent<Button>().interactable = false;
        }
    }

    private IEnumerator TransitionToNextQuestion()
    {
        for (int i = 0; i < optionTexts.Count; i++)
        {
            optionTexts[i].GetComponentInParent<Button>().interactable = false;
        }

        StartCoroutine(progressBar.SetValue(currentQuestionIndex, 1));
        yield return new WaitForSeconds(timeBetweenQuestions);
        LoadQuestion();

        for (int i = 0; i < optionTexts.Count; i++)
        {
            optionTexts[i].GetComponentInParent<Button>().interactable = true;
        }
    }
}
