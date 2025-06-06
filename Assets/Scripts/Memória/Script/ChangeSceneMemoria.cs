using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneMemoria : MonoBehaviour
{
    public void ChangeSceneAction(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}