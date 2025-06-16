using TMPro;
using UnityEngine;

public class TimerScriptMemoria : MonoBehaviour
{
    private TMP_Text timerText;
    private float currentTimer;
    private bool isCounting;

    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        if (timerText == null)
        {
            Debug.LogError("Componente TMP_Text não encontrado!");
        }
        currentTimer = 0;
        isCounting = true;
    }

    void Update()
    {
        if (!isCounting) return;

        currentTimer += Time.deltaTime;
        float seconds = Mathf.FloorToInt(currentTimer % 60);
        float minutes = Mathf.FloorToInt(currentTimer / 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public int GetTimerAndStop()
    {
        isCounting = false;
        return (int)currentTimer;
    }

    public void ResetTimer()
    {
        currentTimer = 0;
        isCounting = true;
    }
}