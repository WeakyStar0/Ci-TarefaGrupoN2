using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceOpacity : MonoBehaviour
{
    [Header("Definições de Opacidade (Respiração)")]
    [SerializeField] private float lowOpacity = 0.3f;
    [SerializeField] private float highOpacity = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float finalOpacity = 1f; // Corrigi para começar em minúscula (boas práticas)

    private Image image;
    private bool isActive = true;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!isActive) return;

        float alpha = Mathf.Lerp(lowOpacity, highOpacity, Mathf.PingPong(Time.time * speed, 1));
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void StopBreathingEffect()
    {
        isActive = false;

        // Aplica a opacidade final definida no Inspetor
        var color = image.color;
        color.a = finalOpacity;
        image.color = color;
    }
}
