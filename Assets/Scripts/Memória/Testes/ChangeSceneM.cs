using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneM : MonoBehaviour
{
    public void ChangeSceneAction(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}