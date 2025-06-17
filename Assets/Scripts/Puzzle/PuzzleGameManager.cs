using UnityEngine;
using UnityEngine.SceneManagement;

public static class PuzzleGameManager
{
    private static int _rightAnswers = 0;
    private static int _wrongAnswers = 0;

    public static int requiredCorrectAnswers = 4;

    public static void IncrementRightAnswer()
    {
        _rightAnswers++;
        PuzzleAudioManager.Instance?.PlayCorrectSound(_rightAnswers); // <-- com pitch dinâmico

        if (_rightAnswers >= requiredCorrectAnswers)
        {
            GameObject.FindObjectOfType<PuzzleTimer>().StopTimer();
            SceneManager.LoadScene("PuzzleResultados");
        }
    }


    public static void IncrementWrongAnswer()
    {
        _wrongAnswers++;

        // Tocar som de resposta errada
        PuzzleAudioManager.Instance?.PlayWrongSound();
    }

    public static int GetRightAnswer()
    {
        return _rightAnswers;
    }

    public static int GetWrongAnswer()
    {
        return _wrongAnswers;
    }

    public static void Reset()
    {
        _rightAnswers = 0;
        _wrongAnswers = 0;
        GameObject.FindObjectOfType<PuzzleTimer>().ResetTimer();
    }
}
