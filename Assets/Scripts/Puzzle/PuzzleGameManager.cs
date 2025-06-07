using UnityEngine.SceneManagement;

public static class PuzzleGameManager
{
    private static int _rightAnswers = 0;
    private static int _wrongAnswers = 0;

    // Novo: Quantas peças corretas são necessárias para vencer o nível
    public static int requiredCorrectAnswers = 4;

    public static void IncrementRightAnswer()
    {
        _rightAnswers++;
        if (_rightAnswers >= requiredCorrectAnswers)
        {
            SceneManager.LoadScene("PuzzleResultados");
        }
    }

    public static void IncrementWrongAnswer()
    {
        _wrongAnswers++;
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
    }
}
