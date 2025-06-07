using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelScore
{
    public int level;
    public int score;
}

[System.Serializable]
public class GameData
{
    public List<LevelScore> levelHighScores = new List<LevelScore>();
}

public class ApanhadaGameManager : MonoBehaviour
{
    [Header("Level Info")]
    public int levelNumber = 1;

    [Header("Game Setup")]
    public GameObject[] holes;
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

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip goodMoleSound;
    public AudioClip badMoleSound;

    private float timeLeft;
    private int score = 0;
    private bool isGameRunning = false;
    private string saveFilePath;
    private GameData gameData;

    void Start()
    {
        levelNumber = PlayerPrefs.GetInt("CurrentLevel", levelNumber);
        saveFilePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
        LoadHighScores();
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

        EndGame();
    }

    void EndGame()
    {
        isGameRunning = false;
        timerText.text = "Time: 0";

        CheckForNewHighScore();

        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.SetInt("LastHighScore", GetHighScoreForLevel(levelNumber));
        PlayerPrefs.SetInt("LastLevel", levelNumber);
        PlayerPrefs.Save();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("ApanhaResultados");
    }

    IEnumerator SpawnMoles()
    {
        while (isGameRunning)
        {
            float wait = Random.Range(minCooldown, maxCooldown);
            yield return new WaitForSeconds(wait);

            if (!isGameRunning) yield break;

            List<GameObject> availableHoles = new List<GameObject>();
            foreach (var hole in holes)
            {
                Transform moleTransform = hole.transform.Find("Mole");
                if (moleTransform != null && !moleTransform.gameObject.activeSelf)
                {
                    availableHoles.Add(hole);
                }
            }

            if (availableHoles.Count == 0) continue;

            GameObject selectedHole = availableHoles[Random.Range(0, availableHoles.Count)];
            GameObject mole = selectedHole.transform.Find("Mole").gameObject;

            MoleBehaviour moleScript = mole.GetComponent<MoleBehaviour>();
            moleScript.apanhadaGameManager = this;

            bool isGood = Random.value > 0.5f;
            moleScript.isGood = isGood;

            mole.GetComponent<Image>().sprite = isGood ? goodMoleSprite : badMoleSprite;

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
        scoreText.text = $"Score: {score}\nHigh Score: {GetHighScoreForLevel(levelNumber)}";
    }

    public void PlayGoodMoleSound()
    {
        if (audioSource != null && goodMoleSound != null)
        {
            audioSource.PlayOneShot(goodMoleSound);
        }
    }

    public void PlayBadMoleSound()
    {
        if (audioSource != null && badMoleSound != null)
        {
            audioSource.PlayOneShot(badMoleSound);
        }
    }

    void SaveHighScores()
    {
        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, jsonData);
    }

    void LoadHighScores()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            gameData = new GameData();
        }
    }

    int GetHighScoreForLevel(int level)
    {
        foreach (var entry in gameData.levelHighScores)
        {
            if (entry.level == level)
                return entry.score;
        }
        return 0;
    }

    void CheckForNewHighScore()
    {
        bool found = false;
        for (int i = 0; i < gameData.levelHighScores.Count; i++)
        {
            if (gameData.levelHighScores[i].level == levelNumber)
            {
                if (score > gameData.levelHighScores[i].score)
                {
                    gameData.levelHighScores[i].score = score;
                    Debug.Log("Novo recorde atualizado para o nível " + levelNumber + ": " + score);
                }
                found = true;
                break;
            }
        }

        if (!found)
        {
            gameData.levelHighScores.Add(new LevelScore { level = levelNumber, score = score });
            Debug.Log("Novo recorde criado para o nível " + levelNumber + ": " + score);
        }

        SaveHighScores();
    }
}
