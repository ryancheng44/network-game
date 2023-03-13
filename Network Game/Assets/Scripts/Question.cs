using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string question;
    public int numberOfCorrectAnswers;
    public List<string> correctAnswers;
    public List<string> wrongAnswers;
}
