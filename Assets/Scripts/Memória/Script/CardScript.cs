using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Image Below;      // O sprite mostrado ao virar a carta
    public GameObject Cover; // O lado "fechado" da carta

    public void SetBelowImage(Sprite sprite)
    {
        Below.sprite = sprite;
    }

    public void DisableCover()
    {
        Cover.SetActive(false);
    }

    public void EnableCover()
    {
        Cover.SetActive(true);
    }
}