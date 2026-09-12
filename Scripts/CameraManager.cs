using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;

    [Header("距離")]
    public float distance = 8f;

    [Header("高度")]
    public float height = 4f;

    [Header("旋轉")]
    public float mouseSensitivity = 3f;

    [Header("平滑")]
    public float smoothSpeed = 10f;

    float yaw;
    float pitch = 35f;

    public bool mobileMode = true;

    Vector2 lookInput;

    void Update()
    {
        if (!GameManager.Instance.roundManager.IsPlaying()) return;
#if UNITY_EDITOR
        // 編輯器用滑鼠測試
        if (!mobileMode)
        {
            MouseInput();
        }
#endif

        MobileInput();
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.roundManager.IsPlaying()) return;
        FollowTarget();
    }

    void MouseInput()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;

        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, 20f, 60f);
    }

    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }


    void MobileInput()
    {
        yaw += lookInput.x * mouseSensitivity;

        pitch -= lookInput.y * mouseSensitivity;


        pitch = Mathf.Clamp(pitch, 20f, 60f);

        // 一幀清掉
        lookInput = Vector2.zero;
    }

    void FollowTarget()
    {
        Quaternion rotation =
            Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset =
            rotation * new Vector3(0, 0, -distance);

        Vector3 targetPosition =
            target.position +
            Vector3.up * height +
            offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(
            target.position + Vector3.up * 1.5f
        );
    }
}