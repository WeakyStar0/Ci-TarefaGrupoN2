using UnityEngine;
using UnityEngine.UI;

public class CardScriptMemoria : MonoBehaviour
{
    public Image Below;
    public GameObject Cover;

    public void SetBelowImage(Sprite sprite)
    {
        if (Below != null)
        {
            Below.sprite = sprite;
        }
    }

    public void DisableCover()
    {
        if (Cover != null)
        {
            Cover.SetActive(false);
        }
    }

    public void EnableCover()
    {
        if (Cover != null)
        {
            Cover.SetActive(true);
        }
    }
}