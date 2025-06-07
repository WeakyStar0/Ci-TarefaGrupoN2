using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalSceneManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rightAnswersText;
    [SerializeField]
    private TMP_Text wrongAnswersText;
    public void Start()
    {
        rightAnswersText.text = PuzzleGameManager.GetRightAnswer().ToString();
        wrongAnswersText.text = PuzzleGameManager.GetWrongAnswer().ToString();
    }
    public void TestAgain()
    {
        PuzzleGameManager.Reset();
        SceneManager.LoadScene("MenuPuzzle");
    }
}
