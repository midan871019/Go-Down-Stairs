using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    public float speedMultiplier = 1f;


    List<SpeedEffect> speedEffects =
        new List<SpeedEffect>();


    void Update()
    {
        UpdateEffects();
    }


    void UpdateEffects()
    {
        for (int i = speedEffects.Count - 1; i >= 0; i--)
        {
            speedEffects[i].time -= Time.deltaTime;


            if (speedEffects[i].time <= 0)
            {
                speedEffects.RemoveAt(i);
            }
        }


        CalculateSpeed();
    }


    void CalculateSpeed()
    {
        float result = 1f;


        foreach (var effect in speedEffects)
        {
            result *= effect.multiplier;
        }


        speedMultiplier = result;
    }



    public void AddSpeedEffect(
        float multiplier,
        float duration
    )
    {
        SpeedEffect effect =
            new SpeedEffect();


        effect.multiplier = multiplier;
        effect.time = duration;


        speedEffects.Add(effect);
    }



    public void ClearEffects()
    {
        speedEffects.Clear();
        speedMultiplier = 1f;
    }
}

public class SpeedEffect
{
    public float multiplier;
    public float time;
}