using UnityEngine;
using TMPro;

public class PuzzleTimer : MonoBehaviour
{
    public TMP_Text timerText; // Arrasta o texto no Inspector
    public static PuzzleTimer Instance;

    private float elapsedTime = 0f;
    private bool isRunning = true;
    public static float finalTime = 0f; // Guarda o tempo final


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
        finalTime = elapsedTime; // Guarda o tempo quando termina
    }


    public void ResetTimer()
    {
        elapsedTime = 0f;
        finalTime = 0f;
        isRunning = true;
    }


    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
