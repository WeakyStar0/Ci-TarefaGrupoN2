using UnityEngine;
using UnityEngine.EventSystems;

public class MoleBehaviour : MonoBehaviour, IPointerClickHandler
{
    public ApanhadaGameManager apanhadaGameManager;
    public bool isGood;
    public AudioSource audioSource;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!gameObject.activeSelf) return;

        if (isGood)
        {
            apanhadaGameManager.AddScore(1);
            apanhadaGameManager.PlayGoodMoleSound();
        }
        else
        {
            apanhadaGameManager.AddScore(-2);
            apanhadaGameManager.PlayBadMoleSound();
        }

        gameObject.SetActive(false);
    }
}
