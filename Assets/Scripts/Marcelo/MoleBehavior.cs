using UnityEngine;
using UnityEngine.EventSystems;

public class MoleBehaviour : MonoBehaviour, IPointerClickHandler
{
    public ApanhadaGameManager apanhadaGameManager;
    public bool isGood;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!gameObject.activeSelf) return;

        if (isGood)
        {
            apanhadaGameManager.AddScore(1);
        }
        else
        {
            apanhadaGameManager.AddScore(-2);
        }

        gameObject.SetActive(false);
    }
}
