using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public float deathY = -50f;

    PlayerHealth health;

    void Start()
    {
        health = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (transform.position.y < deathY)
        {
            health.TakeDamage(9999);
        }
    }
}