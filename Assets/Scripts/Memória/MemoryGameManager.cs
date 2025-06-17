using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryGameManager : MonoBehaviour
{
    [Header("Pontuação")]
    public TextMeshProUGUI scoreText;
    private int score = 0;

    [Header("Configuração do Jogo")]
    [Range(1, 30)]
    public int numberOfPairs = 6; // Número de pares definidos no Inspetor
    public Sprite backSprite;
    public Sprite[] availableCardSprites; // Lista completa de sprites para sortear
    public GameObject cardPrefab;
    public Transform gridParent;

    [Header("Painel de Vitória")]
    public GameObject winPanel;

    private List<CardBehaviour> spawnedCards = new List<CardBehaviour>();
    private CardBehaviour firstSelected = null;
    private CardBehaviour secondSelected = null;
    private bool isChecking = false;

    private int totalPairs;
    private int matchedPairs = 0;

    // Sprites sorteados para a partida atual
    private Sprite[] cardSprites;

    void Start()
    {
        // Garante que o número de pares não exceda os sprites disponíveis
        numberOfPairs = Mathf.Clamp(numberOfPairs, 1, availableCardSprites.Length);

        // Sorteia os sprites que serão usados nesta partida
        List<Sprite> selectedSprites = new List<Sprite>(availableCardSprites);
        Shuffle(selectedSprites);
        cardSprites = selectedSprites.GetRange(0, numberOfPairs).ToArray();

        totalPairs = numberOfPairs;
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

        // Cria os pares duplicando os sprites sorteados
        foreach (var sprite in cardSprites)
        {
            allSprites.Add(sprite);
            allSprites.Add(sprite);
        }

        Shuffle(allSprites);

        // Instancia as cartas
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
            firstSelected.SetMatched();
            secondSelected.SetMatched();

            score += 3;
            matchedPairs++;

            UpdateScoreText();

            if (matchedPairs >= totalPairs)
            {
                GameWon();
            }
        }
        else
        {
            firstSelected.FlipBack();
            secondSelected.FlipBack();

            score -= 1;
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