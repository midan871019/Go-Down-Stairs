using UnityEngine;

public abstract class PlatformBase : MonoBehaviour
{
    [Header("平台大小")]
    public Vector3 platformSize = Vector3.one;

    protected bool playerOnPlatform;

    protected virtual void Awake()
    {
        transform.localScale = platformSize;
    }

    // 玩家踩上
    public virtual void OnPlayerEnter(GameObject player)
    {
        playerOnPlatform = true;
    }

    // 玩家離開
    public virtual void OnPlayerExit(GameObject player)
    {
        playerOnPlatform = false;
    }

    // 初始化
    public virtual void Setup(Vector3 size)
    {
        platformSize = size;

        transform.localScale = platformSize;
        transform.localScale = new Vector3(size.x, size.y, size.z);
    }

    // 回收
    public virtual void Recycle()
    {
        gameObject.SetActive(false);
    }
}