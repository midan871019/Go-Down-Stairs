using UnityEngine;

public class NormalPlatform : PlatformBase
{
    [Header("回血")]
    public float healAmount = 5f;

    bool healed;

    public override void OnPlayerEnter(GameObject player)
    {
        base.OnPlayerEnter(player);
        Debug.Log("站上一般平台");

        if (healed)
            return;

        PlayerHealth health =
            player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.Heal(healAmount);

            healed = true;
        }
    }

    public override void OnPlayerExit(GameObject player)
    {
        base.OnPlayerExit(player);
    }

    public override void Setup(Vector3 size)
    {
        base.Setup(size);

        healed = false;
    }
}