using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private void Awake()
    {
        // Regista callback para sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desregista callback para evitar leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Garantir que o cursor está visível e desbloqueado sempre que uma cena carregar
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void PlayClickSound()
    {
        UIAudioManager.Instance?.PlayClickSound();
    }

    public void LoadSceneByName(string sceneName)
    {
        PlayClickSound();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        PlayClickSound();
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoBack()
    {
        PlayClickSound();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 0)
        {
            SceneManager.LoadScene(currentSceneIndex - 1);
        }
        else
        {
            Debug.Log("Já estás na primeira cena, não dá para voltar mais.");
        }
    }

    public void ReloadCurrentScene()
    {
        PlayClickSound();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void QuitApp()
    {
        PlayClickSound();
        Application.Quit();
        Debug.Log("Aplicação fechada.");
    }
}
