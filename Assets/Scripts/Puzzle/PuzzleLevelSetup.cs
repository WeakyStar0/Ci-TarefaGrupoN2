using UnityEngine;

public class PuzzleLevelSetup : MonoBehaviour
{
    [SerializeField] private int correctAnswersNeeded = 6;

    void Start()
    {
        PuzzleGameManager.Reset();
        PuzzleGameManager.requiredCorrectAnswers = correctAnswersNeeded;
    }
}
