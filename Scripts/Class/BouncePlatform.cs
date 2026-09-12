using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : PlatformBase
{
    public float jumpPower = 12f;

    public override void OnPlayerEnter(GameObject player)
    {
        base.OnPlayerEnter(player);

        GameManager.Instance.playerMovementManager.Bounce(jumpPower);
    }
}
