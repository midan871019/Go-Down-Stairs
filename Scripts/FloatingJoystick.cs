using UnityEngine.EventSystems;
using UnityEngine;

public class FloatingJoystick : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    public float handleRange = 80f;

    [HideInInspector]
    public Vector2 input;

    Canvas canvas;
    Camera uiCamera;

    void Start()
    {
        GameManager.Instance.roundManager.OnGameStart += StartSet;
        
    }
    public void StartSet()
    {
        canvas = GetComponentInParent<Canvas>();

        uiCamera = canvas.worldCamera;

        background.gameObject.SetActive(false);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);

        background.position = eventData.position;

        handle.position = eventData.position;

        input = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction =
            eventData.position -
            (Vector2)background.position;

        direction =
            Vector2.ClampMagnitude(
                direction,
                handleRange
            );

        handle.position =
            (Vector2)background.position +
            direction;

        input = direction / handleRange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);

        input = Vector2.zero;
    }
}
