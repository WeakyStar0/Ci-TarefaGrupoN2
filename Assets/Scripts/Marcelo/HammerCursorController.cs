using UnityEngine;
using UnityEngine.UI;

public class HammerCursorController : MonoBehaviour
{
    public float slamRotation = -45f; // How far it rotates on click
    public float slamSpeed = 10f;     // How fast it slams
    public float resetSpeed = 5f;     // How fast it resets

    private RectTransform rectTransform;
    private bool isSlamming = false;
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalRotation = rectTransform.rotation;
        targetRotation = originalRotation;

        // Hide system cursor
        Cursor.visible = false;
    }

    void Update()
    {
        FollowMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Slam();
        }

        RotateHammer();
    }

    void FollowMouse()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition,
            null,
            out pos
        );

        rectTransform.anchoredPosition = pos;
    }

    void Slam()
    {
        isSlamming = true;
        targetRotation = Quaternion.Euler(0f, 0f, slamRotation);
    }

    void RotateHammer()
    {
        if (isSlamming)
        {
            rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, targetRotation, Time.deltaTime * slamSpeed);

            if (Quaternion.Angle(rectTransform.rotation, targetRotation) < 1f)
            {
                isSlamming = false;
                targetRotation = originalRotation;
            }
        }
        else
        {
            rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, targetRotation, Time.deltaTime * resetSpeed);
        }
    }
}
