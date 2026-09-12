using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatform : PlatformBase
{
    public float multiplier = 1.5f;

    public float duration = 3f;


    public override void OnPlayerEnter(GameObject player)
    {
        base.OnPlayerEnter(player);

        GameManager.Instance.playerEffectManager.AddSpeedEffect(multiplier, duration);
    }

}
