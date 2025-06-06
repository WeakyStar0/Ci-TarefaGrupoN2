using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    // Método público para carregar uma cena pelo nome (para ligar ao botão)
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Método para carregar uma cena pelo índice (opcional)
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Método para voltar à cena anterior
    public void GoBack()
    {
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

    // Método para reiniciar a cena atual
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Método para fechar a aplicação (funciona só no build)
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Aplicação fechada.");
    }
}
