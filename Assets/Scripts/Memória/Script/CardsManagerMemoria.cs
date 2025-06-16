using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardsManagerMemoria : MonoBehaviour
{
    [SerializeField]
    private List<CardScriptMemoria> listOfCards;

    [SerializeField]
    private string spritesFolder = "Below";

    [SerializeField]
    private AudioSource victoryMusic;

    [SerializeField]
    private TimerScriptMemoria timerScript;

    private List<Sprite> sprites = new List<Sprite>();
    private CardScriptMemoria firstSelectedItem;
    private CardScriptMemoria secondSelectedItem;
    private int numberOfMatches = 0;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (listOfCards == null || listOfCards.Count == 0)
        {
            Debug.LogError("Lista de cartas não configurada!");
        }
    }

    public void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
        LoadSpritesFromFolder();

        if (listOfCards.Count / 2 != sprites.Count)
        {
            throw new ApplicationException($"A configuração está errada: {listOfCards.Count} cartas precisam de {listOfCards.Count / 2} sprites, mas encontramos {sprites.Count} sprites na pasta.");
        }

        for (int i = 0; i < listOfCards.Count; i++)
        {
            listOfCards[i].SetBelowImage(sprites[i / 2]);
        }

        Shuffle(listOfCards);
    }

    private void LoadSpritesFromFolder()
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(spritesFolder);

        if (loadedSprites == null || loadedSprites.Length == 0)
        {
            throw new ApplicationException($"Nenhum sprite encontrado na pasta '{spritesFolder}' dentro de Resources.");
        }

        sprites.AddRange(loadedSprites);
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        for (int i = 0; i < listOfCards.Count; i++)
        {
            listOfCards[i].transform.SetSiblingIndex(i);
        }
    }

    public void OnCardClick()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        if (firstSelectedItem && secondSelectedItem)
            return;

        var clickedItem = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CardScriptMemoria>();

        if (clickedItem == null || clickedItem == firstSelectedItem)
            return;

        if (!firstSelectedItem)
        {
            firstSelectedItem = clickedItem;
            firstSelectedItem.DisableCover();
        }
        else
        {
            secondSelectedItem = clickedItem;
            secondSelectedItem.DisableCover();
            CompareChosenItems();
        }
    }

    private void CompareChosenItems()
    {
        Sprite spriteA = firstSelectedItem.Below.sprite;
        Sprite spriteB = secondSelectedItem.Below.sprite;

        if (spriteA == spriteB)
        {
            numberOfMatches++;
            StartCoroutine(ResetAndCheckFinish(0, false));
        }
        else
        {
            StartCoroutine(ResetAndCheckFinish(2, true));
        }
    }

    IEnumerator ResetAndCheckFinish(int numberOfSecondsToWait, bool shouldReset)
    {
        canvasGroup.interactable = false;
        yield return new WaitForSeconds(numberOfSecondsToWait);

        if (shouldReset)
        {
            firstSelectedItem.EnableCover();
            secondSelectedItem.EnableCover();
        }

        firstSelectedItem = null;
        secondSelectedItem = null;
        canvasGroup.interactable = true;

        if (numberOfMatches == listOfCards.Count / 2)
        {
            StartCoroutine(LoadFinalScene());
        }
    }

    private IEnumerator LoadFinalScene()
    {
        MemoriaGameManager.SetSeconds(timerScript.GetTimerAndStop());
        victoryMusic.Play();
        yield return new WaitForSeconds(victoryMusic.clip.length);

        if (MemoriaGameManager.GetCurrentLevel() < 3)
        {
            MemoriaGameManager.SetNextLevel();
            SceneManager.LoadScene("MemoriaLevel" + MemoriaGameManager.GetCurrentLevel());
        }
        else
        {
            SceneManager.LoadScene("MemoriaFinalScene");
        }
    }

    public void ResetGame()
    {
        numberOfMatches = 0;
        firstSelectedItem = null;
        secondSelectedItem = null;
        timerScript.ResetTimer();
        Shuffle(listOfCards);
        
        foreach (var card in listOfCards)
        {
            card.EnableCover();
        }
    }
}