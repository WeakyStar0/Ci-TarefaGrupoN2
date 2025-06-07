using UnityEngine;
using TMPro;

public class ResultadosDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI levelText;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        int score = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("LastHighScore", 0);
        int level = PlayerPrefs.GetInt("LastLevel", 1);

        scoreText.text = "Pontuação: " + score;
        highScoreText.text = "Recorde: " + highScore;
        levelText.text = "Nível: " + level;
    }
}
