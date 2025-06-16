using UnityEngine;

public class MemoriaGameManager : MonoBehaviour
{
    private static int seconds;
    private static int currentLevel = 1;
    private const int MAX_LEVEL = 3;

    public static void SetSeconds(int newSeconds)
    {
        seconds = newSeconds;
    }

    public static int GetSeconds()
    {
        return seconds;
    }

    public static int GetCurrentLevel()
    {
        return currentLevel;
    }

    public static void SetNextLevel()
    {
        if (currentLevel < MAX_LEVEL)
        {
            currentLevel++;
        }
    }

    public static bool IsLastLevel()
    {
        return currentLevel >= MAX_LEVEL;
    }

    public static void Reset()
    {
        seconds = 0;
        currentLevel = 1;
    }
}