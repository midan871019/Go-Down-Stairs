using UnityEngine;
using UnityEngine.EventSystems;

public class RightTouchJump :
    MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{

    Vector2 lastPos;

    public void OnPointerDown(
        PointerEventData eventData
    )
    {
        GameManager.Instance.playerMovementManager.Jump();
        lastPos =
            eventData.position;
    }

    public void OnDrag(
        PointerEventData eventData
    )
    {
        Vector2 delta = (eventData.position - lastPos) * 0.2f;


        GameManager.Instance.cameraManager.SetLookInput(delta);


        lastPos =
            eventData.position;
    }


    public void OnPointerUp(
        PointerEventData eventData
    )
    {

    }
}
