using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApanhadaGameManager : MonoBehaviour
{
    [Header("Game Setup")]
    public GameObject[] holes; // Drag UI hole GameObjects here (each must have a child called "Mole")
    public Sprite goodMoleSprite;
    public Sprite badMoleSprite;
    public float moleVisibleTime = 1.0f;

    [Header("Spawn Timing")]
    public float minCooldown = 0.5f;
    public float maxCooldown = 1.5f;

    [Header("Game Timer")]
    public float gameDuration = 10f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    private float timeLeft;
    private int score = 0;
    private bool isGameRunning = false;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        score = 0;
        UpdateScoreText();

        timeLeft = gameDuration;
        isGameRunning = true;

        StartCoroutine(GameLoop());
        StartCoroutine(SpawnMoles());
    }

    IEnumerator GameLoop()
    {
        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft).ToString();
            yield return null;
        }

        isGameRunning = false;
        timerText.text = "Time: 0";
    }

IEnumerator SpawnMoles()
{
    while (isGameRunning)
    {
        float wait = Random.Range(minCooldown, maxCooldown);
        yield return new WaitForSeconds(wait);

        if (!isGameRunning) yield break;

        // Get list of available (inactive) moles
        List<GameObject> availableHoles = new List<GameObject>();
        foreach (var hole in holes)
        {
            Transform tempMoleTransform = hole.transform.Find("Mole");
            if (tempMoleTransform != null && !tempMoleTransform.gameObject.activeSelf)
            {
                availableHoles.Add(hole);
            }
        }

        if (availableHoles.Count == 0) continue;

        // Choose random hole from available ones
        GameObject selectedHole = availableHoles[Random.Range(0, availableHoles.Count)];
        Transform selectedMoleTransform = selectedHole.transform.Find("Mole");
        GameObject mole = selectedMoleTransform.gameObject;

        MoleBehaviour moleScript = mole.GetComponent<MoleBehaviour>();
        moleScript.apanhadaGameManager = this;

        bool isGood = Random.value > 0.5f;
        moleScript.isGood = isGood;

        Image moleImage = mole.GetComponent<Image>();
        moleImage.sprite = isGood ? goodMoleSprite : badMoleSprite;

        mole.SetActive(true);
        StartCoroutine(HideMoleAfterTime(mole, moleVisibleTime));
    }
}

    IEnumerator HideMoleAfterTime(GameObject mole, float delay)
    {
        yield return new WaitForSeconds(delay);
        mole.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    internal static void SetSeconds(int v)
    {
        throw new System.NotImplementedException();
    }
}
