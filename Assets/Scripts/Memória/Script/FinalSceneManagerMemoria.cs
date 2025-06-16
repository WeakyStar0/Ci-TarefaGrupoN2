using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalSceneManagerMemoria : MonoBehaviour
{
    [SerializeField]
    private TMP_Text answersText;

    public void Start()
    {
        int seconds = MemoriaGameManager.GetSeconds();
        answersText.text = $"Nível {MemoriaGameManager.GetCurrentLevel()} completo em {seconds / 60:00}:{seconds % 60:00}";
    }

    public void PlayAgain()
    {
        MemoriaGameManager.Reset();
        SceneManager.LoadScene("MemoriaMenu");
    }
}