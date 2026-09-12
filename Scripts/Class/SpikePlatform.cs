using UnityEngine;

public class SpikePlatform : PlatformBase
{
    [Header("¨C¬í¶Ë®`")]
    public float damagePerSecond = 15f;
    public float damagePerTime = 15f;

    PlayerHealth currentPlayer;

    void Update()
    {
        if (currentPlayer != null)
        {
            currentPlayer.TakeDamage(
                damagePerSecond * Time.deltaTime
            );
        }
    }

    public override void OnPlayerEnter(GameObject player)
    {
        base.OnPlayerEnter(player);

        currentPlayer =
            player.GetComponent<PlayerHealth>();
        currentPlayer.TakeDamage(damagePerSecond);
        damagePerTime = 0;
    }

    public override void OnPlayerExit(GameObject player)
    {
        base.OnPlayerExit(player);

        currentPlayer = null;
    }
}