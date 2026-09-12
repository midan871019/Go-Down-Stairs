using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    PlatformBase platform;

    void Awake()
    {
        platform = GetComponentInParent<PlatformBase>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("碰到：" + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家進入");

            platform.OnPlayerEnter(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platform.OnPlayerExit(other.gameObject);
        }
    }
}