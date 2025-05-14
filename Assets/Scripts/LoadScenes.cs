using UnityEngine;

public class LoadScenes : MonoBehaviour
{
    // The name of the scene to load
    [SerializeField]
    private string sceneToLoad = "MainMenu";

    
    private void Start()
    {
        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
