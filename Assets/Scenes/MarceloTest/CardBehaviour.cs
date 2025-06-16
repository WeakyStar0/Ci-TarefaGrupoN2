using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    private Image image;
    private Button button;
    private Sprite hiddenSprite;
    private Sprite shownSprite;
    private MemoryGameManager gameManager;
    private bool isRevealed = false;
    private bool isMatched = false;

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetCard(Sprite shownSprite, MemoryGameManager manager)
    {
        this.shownSprite = shownSprite;
        this.hiddenSprite = manager.backSprite;
        this.gameManager = manager;
        FlipBack();
    }

    public void OnClick()
    {
        // Impede clique se já está revelada, se já foi feita correspondência ou se o jogo está a verificar um par
        if (isRevealed || isMatched || gameManager.IsChecking()) return;

        Reveal();
        gameManager.CardClicked(this);
    }

    public void Reveal()
    {
        isRevealed = true;
        image.sprite = shownSprite;
    }

    public void FlipBack()
    {
        isRevealed = false;
        image.sprite = hiddenSprite;
    }

    public Sprite GetSprite()
    {
        return shownSprite;
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    public bool IsMatched()
    {
        return isMatched;
    }
}
