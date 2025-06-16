using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryGameManager : MonoBehaviour
{
    //score text
    public TextMeshProUGUI scoreText; // texto para mostrar a pontuação
    private int score = 0; // variável para armazenar a pontuação
    public Sprite backSprite; // imagem preta
    public Sprite[] cardSprites; // imagens dos pares
    public GameObject cardPrefab;
    public Transform gridParent;

    private List<CardBehaviour> spawnedCards = new List<CardBehaviour>();
    private CardBehaviour firstSelected = null;
    private CardBehaviour secondSelected = null;
    private bool isChecking = false;

    void Start()
    {
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

            score += 3; // Adiciona pontos ao score
            UpdateScoreText();
        }
        else
        {
            // Cartas erradas: voltam para trás
            firstSelected.FlipBack();
            secondSelected.FlipBack();

            score -= 1; // Subtrai pontos do score
            UpdateScoreText();
        }

        firstSelected = null;
        secondSelected = null;
        isChecking = false;
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
