using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.1f;
    public float magnitude = 0.2f;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float t = 0;

        while (t < duration)
        {
            transform.localPosition =
                originalPos + Random.insideUnitSphere * magnitude;

            t += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
