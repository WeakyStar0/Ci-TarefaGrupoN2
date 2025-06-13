using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropPieces : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        var collisionElement = eventData.pointerDrag.GetComponent<PuzzlePiece>();
        if (collisionElement == null) return;

        if (collisionElement.targetImage.name == this.gameObject.name)
        {
            var currentColor = this.GetComponent<Image>().color;
            currentColor.a = 1f;
            GetComponent<Image>().color = currentColor;

            // PARA A OPACIDADE AQUI
            var opacityScript = GetComponent<PuzzlePieceOpacity>();
            if (opacityScript != null)
            {
                opacityScript.StopBreathingEffect();
            }

            Destroy(collisionElement.gameObject, 0);
            PuzzleGameManager.IncrementRightAnswer();
        }
        else
        {
            collisionElement.ResetImage();
            PuzzleGameManager.IncrementWrongAnswer();
        }
    }
}
