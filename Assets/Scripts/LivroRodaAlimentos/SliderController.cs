using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public GameObject[] slides; // Lista de slides
    private int currentIndex = 0;

    public void ShowNextSlide()
    {
        slides[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % slides.Length;
        slides[currentIndex].SetActive(true);
    }

public void ShowPreviousSlide()
{
    slides[currentIndex].SetActive(false);
    currentIndex--;

    if (currentIndex < 0)
        currentIndex = slides.Length - 1;

    slides[currentIndex].SetActive(true);
}
}
