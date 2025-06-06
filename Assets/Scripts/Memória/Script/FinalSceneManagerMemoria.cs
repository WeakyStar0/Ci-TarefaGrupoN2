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
        answersText.text = MemoriaGameManager.GetSeconds().ToString();
    }
    public void TestAgain()
    {
        MemoriaGameManager.Reset();
        SceneManager.LoadScene("MainScene");
    }
}