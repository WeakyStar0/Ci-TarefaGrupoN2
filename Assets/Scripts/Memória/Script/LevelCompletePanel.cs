using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button menuButton;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int currentLevel = MemoriaGameManager.GetCurrentLevel();
        int seconds = MemoriaGameManager.GetSeconds();
        
        levelText.text = $"Nível {currentLevel} Completo!";
        timeText.text = $"Tempo: {seconds / 60:00}:{seconds % 60:00}";
        
        nextLevelButton.gameObject.SetActive(!MemoriaGameManager.IsLastLevel());
    }

    public void NextLevel()
    {
        MemoriaGameManager.SetNextLevel();
        SceneManager.LoadScene($"MemoriaLevel{MemoriaGameManager.GetCurrentLevel()}");
    }

    public void ReturnToMenu()
    {
        MemoriaGameManager.Reset();
        SceneManager.LoadScene("MemoriaMenu");
    }
}