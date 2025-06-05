using UnityEngine;
using UnityEngine.EventSystems;

public class MoleBehaviour : MonoBehaviour, IPointerClickHandler
{
    public GameManager gameManager;
    public bool isGood;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!gameObject.activeSelf) return;

        if (isGood)
        {
            gameManager.AddScore(1);
        }
        else
        {
            gameManager.AddScore(-2);
        }

        gameObject.SetActive(false);
    }
}
