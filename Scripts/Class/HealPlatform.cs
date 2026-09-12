using UnityEngine;

public class HealPlatform : PlatformBase
{
    [Header("¨C¬í¦^¦å")]
    public float healPerSecond = 10f;
    public float healPerTime = 10f;

    PlayerHealth currentPlayer;

    void Update()
    {
        if (currentPlayer != null)
        {
            currentPlayer.Heal(
                healPerSecond * Time.deltaTime
            );
        }
    }

    public override void OnPlayerEnter(GameObject player)
    {
        base.OnPlayerEnter(player);

        currentPlayer =
            player.GetComponent<PlayerHealth>();
        currentPlayer.Heal(healPerTime);
        healPerTime = 0;
    }

    public override void OnPlayerExit(GameObject player)
    {
        base.OnPlayerExit(player);

        currentPlayer = null;
    }
}