using UnityEngine;

public class TopBoundary : MonoBehaviour
{
    public Renderer visual;

    public Color normalColor = Color.clear;
    public Color dangerColor = Color.red;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            visual.material.color = dangerColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            visual.material.color = normalColor;
        }
    }

    public float damagePerSecond = 20f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health =
                other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(
                    damagePerSecond *
                    Time.deltaTime
                );
            }
        }
    }
}