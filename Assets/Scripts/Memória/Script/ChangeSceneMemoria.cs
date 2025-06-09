using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneMemoria : MonoBehaviour
{
    public void ChangeSceneMenu(string sceneName)
    {
        SceneManager.LoadScene("MemoriaMenu");
    }
}