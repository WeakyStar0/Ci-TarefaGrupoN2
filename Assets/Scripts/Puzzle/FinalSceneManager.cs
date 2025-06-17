using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rightAnswersText;
    [SerializeField]
    private TMP_Text wrongAnswersText;
    [SerializeField]
    private TMP_Text timeText;

    public void Start()
    {
        rightAnswersText.text = PuzzleGameManager.GetRightAnswer().ToString();
        wrongAnswersText.text = PuzzleGameManager.GetWrongAnswer().ToString();

        float elapsedTime = PuzzleTimer.finalTime; // Pega no tempo guardado
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void TestAgain()
    {
        PuzzleGameManager.Reset();
        SceneManager.LoadScene("MenuPuzzle");
    }
}
