using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    private List<CardScript> listOfCards;

    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private AudioSource victoryMusic;

    [SerializeField]
    private TimerScript timerScript;

    private CardScript firstSelectedItem;
    private CardScript secondSelectedItem;
    private int numberOfMatches = 0;
    private CanvasGroup canvasGroup;

    public void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();

        if (listOfCards.Count / 2 != sprites.Count)
        {
            throw new ApplicationException("A configuração do GameManager está errada: número de pares de cartas não corresponde ao número de sprites.");
        }

        // Atribuir sprites (dois cartões por sprite)
        for (int i = 0; i < listOfCards.Count; i++)
        {
            listOfCards[i].SetBelowImage(sprites[i / 2]);
        }

        Shuffle(listOfCards);
    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
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

        var clickedItem = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CardScript>();

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

    IEnumerator LoadFinalScene()
    {
        GameManager.SetSeconds(timerScript.GetTimerAndStop());
        victoryMusic.Play();
        yield return new WaitForSeconds(victoryMusic.clip.length);
        SceneManager.LoadScene("FinalScene");
    }
}