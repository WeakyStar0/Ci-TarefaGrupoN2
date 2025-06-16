using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryGameManager : MonoBehaviour
{
    // Score
    public TextMeshProUGUI scoreText;
    private int score = 0;

    // Cartas
    public Sprite backSprite;
    public Sprite[] cardSprites;
    public GameObject cardPrefab;
    public Transform gridParent;

    private List<CardBehaviour> spawnedCards = new List<CardBehaviour>();
    private CardBehaviour firstSelected = null;
    private CardBehaviour secondSelected = null;
    private bool isChecking = false;

    // Painel de vitória
    public GameObject winPanel;

    // Contagem de pares
    private int totalPairs;
    private int matchedPairs = 0;

    void Start()
    {
        totalPairs = cardSprites.Length;
        matchedPairs = 0;

        winPanel.SetActive(false);
        UpdateScoreText();
        SetupCards();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Pontos: " + score.ToString();
    }

    public bool IsChecking()
    {
        return isChecking;
    }

    void SetupCards()
    {
        List<Sprite> allSprites = new List<Sprite>();

        // Duplicar pares
        foreach (var sprite in cardSprites)
        {
            allSprites.Add(sprite);
            allSprites.Add(sprite);
        }

        Shuffle(allSprites);

        foreach (var sprite in allSprites)
        {
            GameObject cardObj = Instantiate(cardPrefab, gridParent);
            CardBehaviour card = cardObj.GetComponent<CardBehaviour>();
            card.SetCard(sprite, this);
            spawnedCards.Add(card);
        }
    }

    public void CardClicked(CardBehaviour clickedCard)
    {
        if (isChecking) return;

        if (firstSelected == null)
        {
            firstSelected = clickedCard;
        }
        else if (secondSelected == null && clickedCard != firstSelected)
        {
            secondSelected = clickedCard;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isChecking = true;
        yield return new WaitForSeconds(1f);

        if (firstSelected.GetSprite() == secondSelected.GetSprite())
        {
            // Cartas corretas: mantêm-se viradas
            firstSelected.SetMatched();
            secondSelected.SetMatched();

            score += 3; // Pontos
            matchedPairs++;

            UpdateScoreText();

            // Verifica se ganhou
            if (matchedPairs >= totalPairs)
            {
                GameWon();
            }
        }
        else
        {
            // Cartas erradas: voltam para trás
            firstSelected.FlipBack();
            secondSelected.FlipBack();

            score -= 1; // Perde pontos
            UpdateScoreText();
        }

        firstSelected = null;
        secondSelected = null;
        isChecking = false;
    }

    private void GameWon()
    {
        winPanel.SetActive(true);
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
